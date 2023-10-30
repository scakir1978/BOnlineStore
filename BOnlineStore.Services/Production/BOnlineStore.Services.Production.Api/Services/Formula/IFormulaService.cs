using BOnlineStore.Generic.Service;
using BOnlineStore.Services.Production.Api.Dtos;
using BOnlineStore.Services.Production.Api.Entities;
using System.Linq.Expressions;

namespace BOnlineStore.Services.Production.Api.Services
{
    public interface IFormulaService : IService<Formula, FormulaDto, FormulaCreateDto, FormulaUpdateDto>
    {
        /// <summary>
        /// Formüş girişi ekranında tanımlanan formülün, 
        /// düzgün oluşup oluşmadığını anlamak için, sanal değerler ile formül oluşturulup çalıştırılır.
        /// Eğer formül düzgün oluşmuş ise formül sanal değerler ile hata vermeden çalışır.
        /// Formülün içinde EN1, EN2, EN3, YUKSEKLIK ve SONUCDEGISKENI için "100" değeri kullanılır. 
        /// SABIT içinse girilen değer kullanılır.        
        /// </summary>
        /// <param name="formulaDetails"></param>
        /// <returns></returns>
        Task<bool> ExecuteFormulaTest(List<FormulaDetail> formulaDetail);

        /// <summary>
        /// Seçilen formülü, yine seçilen modele kopyalar.
        /// </summary>
        /// <param name="formulaId">Kopyalanacak formülün idsi</param>
        /// <param name="modelId">Formülün kopyalanacağı modelin idsi</param>        
        Task<bool> CopyFormula(string formulaId, string modelId);
    }
}
