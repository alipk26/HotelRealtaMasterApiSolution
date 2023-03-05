﻿using Realta.Domain.Entities;
using Realta.Domain.RequestFeatures;

namespace Realta.Domain.Repositories
{
    public interface IPurchaseOrderRepository
    {
        Task<IEnumerable<PurchaseOrderHeader>> FindAllAsync();
        Task<PagedList<PurchaseOrderHeader>> GetAllAsync(PurchaseOrderParameters param);
        PurchaseOrderNested FindAllDet(string po);
        PurchaseOrderHeader FindById(int id);
        PurchaseOrderDetail FindDetById(int id);
        PurchaseOrderHeader FindByPo(string po);
        void Insert(PurchaseOrderHeader header, PurchaseOrderDetail detail);
        void UpdateStatus(PurchaseOrderHeader header);
        void UpdateQty(PurchaseOrderDetail purchaseOrderDetail);
        void Remove(PurchaseOrderHeader header);
        void RemoveDetail(PurchaseOrderDetail detail);
    }
}
