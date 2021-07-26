using System;

namespace HaloShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable //giao tiếp khởi tạo các đối tượng entity
    {
        HaloShopDbContext Init();
    }
}