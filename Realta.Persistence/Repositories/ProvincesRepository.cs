﻿using Realta.Domain.Entities;
using Realta.Domain.Repositories;
using Realta.Domain.RequestFeatures;
using Realta.Persistence.Base;
using Realta.Persistence.RepositoryContext;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realta.Persistence.Repositories.RepositoryExtensions;

namespace Realta.Persistence.Repositories
{
    internal class ProvincesRepository : RepositoryBase<Provinces>, IProvincesRepository
    {
        public ProvincesRepository(AdoDbContext adoContext) : base(adoContext)
        {
        }

        public void Edit(Provinces provinces)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "UPDATE master.provinces SET prov_name=@prov_name,prov_country_id=@prov_country_id WHERE prov_id= @prov_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_id",
                        DataType = DbType.Int32,
                        Value = provinces.ProvId
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_name",
                        DataType = DbType.String,
                        Value = provinces.ProvName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_country_id",
                        DataType = DbType.Int32,
                        Value = provinces.ProvCountryId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }

        public IEnumerable<Provinces> FindAllProvinces()
        {
            IEnumerator<Provinces> dataset = FindAll<Provinces>("SELECT prov_id as ProvId," +
                "                                                       prov_name as ProvName," +
                "                                                       prov_country_id as ProvCountryId " +
                "                                                FROM master.Provinces ORDER BY prov_id;");

            while (dataset.MoveNext())
            {
                var data = dataset.Current;
                yield return data;
            }
        }

        public Task<IEnumerable<Provinces>> FindAllProvincesAsync()
        {
            throw new NotImplementedException();
        }


        public Provinces FindProvincesById(int id)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT prov_id as ProvId," +
                "                     prov_name as ProvName," +
                "                     prov_country_id as ProvCountryId " +
                "              FROM master.provinces where prov_id=@prov_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_id",
                        DataType = DbType.Int32,
                        Value = id
                    }
                }
            };

            var dataSet = FindByCondition<Provinces>(model);

            Provinces? item = dataSet.Current;

            while (dataSet.MoveNext())
            {
                item = dataSet.Current;
            }
            return item;
        }

        public void Insert(Provinces provinces)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "INSERT INTO master.provinces (prov_name,prov_country_id) values (@prov_name,@prov_country_id);SELECT cast(scope_identity() as int)",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_name",
                        DataType = DbType.String,
                        Value = provinces.ProvName
                    },
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_country_id",
                        DataType = DbType.Int32,
                        Value = provinces.ProvCountryId
                    }
                }
            };
            //_adoContext.ExecuteNonQuery(model);
            provinces.ProvId = _adoContext.ExecuteScalar<int>(model);
            _adoContext.Dispose();
        }

        public void Remove(Provinces provinces)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "DELETE FROM master.provinces WHERE prov_id=@prov_id;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[] {
                    new SqlCommandParameterModel() {
                        ParameterName = "@prov_id",
                        DataType = DbType.Int32,
                        Value = provinces.ProvId
                    }
                }
            };
            _adoContext.ExecuteNonQuery(model);
            _adoContext.Dispose();
        }
        public async Task<IEnumerable<Provinces>> GetProvincesPaging(ProvincesParameter provincesParameter)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT prov_id as ProvId," +
                              "   prov_name as ProvName," +
                              "FROM master.provinces order by prov_id "+
                              "OFFSET @pageNo ROWS FETCH NEXT  @pageSize ROWS ONLY;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]
                {
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@pageNo",
                        DataType = DbType.Int32,
                        Value = provincesParameter.PageNumber
                    },
                    new SqlCommandParameterModel()
                    {
                        ParameterName = "@pagesSize",
                        DataType = DbType.Int32,
                        Value = provincesParameter.PageSize
                    }
                }
            };

            IAsyncEnumerator<Provinces> dataSet = FindAllAsync<Provinces>(model);

            var item = new List<Provinces>();

            while (await dataSet.MoveNextAsync())
            {
                item.Add(dataSet.Current);
            }

            return item;
        }

        public async Task<PagedList<Provinces>> GetProvincesPageList(ProvincesParameter provincesParameter)
        {
            SqlCommandModel model = new SqlCommandModel()
            {
                CommandText = "SELECT prov_id as ProvId, " +
                              "  prov_name as ProvName," +
                              "prov_country_id as ProvCountryId " +
                              "FROM master.provinces order by prov_id ",

                // "OFFSET @pageNo ROWS FETCH NEXT  @pageSize ROWS ONLY;",
                CommandType = CommandType.Text,
                CommandParameters = new SqlCommandParameterModel[]{}
            };
            var provinces = await GetAllAsync<Provinces>(model);

            provinces = provinces.AsQueryable()
                .SearchProvinces(provincesParameter.SearchTerm)
                .Sort(provincesParameter.OrderBy);

            return PagedList<Provinces>.ToPagedList(provinces.ToList(), provincesParameter.PageNumber,
                provincesParameter.PageSize);
        }
    }
}

