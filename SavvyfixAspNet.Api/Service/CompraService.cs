using SavvyfixAspNet.Api.Models;
using SavvyfixAspNet.Domain.Entities;

namespace SavvyfixAspNet.Api.Service;

public static class CompraService
{
    public static Compra MapToCompra(this CompraAddOrUpdateModel compraModel)
    {
        return new Compra()
        {
            QntdProd = compraModel.QntdProd,
            ValorCompra = compraModel.ValorCompra,
            IdProd = compraModel.IdProd,
            IdCliente = compraModel.IdCliente,
            IdAtividades = compraModel.IdAtividades,
            NmProd = compraModel.NmProd,
            EspcificacoesProd = compraModel.EspcificacoesProd
        };
    }
}