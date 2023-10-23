using BOnlineStore.Shared.Entity;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BOnlineStore.Services.Production.Api.Dtos
{
    /// <summary>
    /// Formül tipleri için kullanılacak dto
    /// </summary>
    public class FormulaTypeDto : EntityDto
    {
        /// <summary>
        /// Formül tipi kodu
        /// </summary>
        public string? Code { get; set; }

        /// <summary>
        /// Formül tipi adı
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Formül tipi için değişken-1 bilgisi girilecek
        /// </summary>
        public bool? IsVariable1Required { get; private set; }

        /// <summary>
        /// Formül tipi için değişken-2 bilgisi girilecek
        /// </summary>
        public bool? IsVariable2Required { get; private set; }

        /// <summary>
        /// Formül tipi için değişken-3 bilgisi girilecek
        /// </summary>
        public bool? IsVariable3Required { get; private set; }

        /// <summary>
        /// Formül tipi için değişken-4 bilgisi girilecek
        /// </summary>
        public bool? IsVariable4Required { get; private set; }

        /// <summary>
        /// Formül tipi için sabit-1 bilgisi girilecek
        /// </summary>
        public bool? IsConstant1Required { get; private set; }

        /// <summary>
        /// Formül tipi için sabit-2 bilgisi girilecek
        /// </summary>
        public bool? IsConstant2Required { get; private set; }

        public FormulaTypeDto(string id, string code, string name, bool? isVariable1Required = null,
                              bool? isVariable2Required = null, bool? isVariable3Required = null,
                              bool? isVariable4Required = null, bool? isConstant1Required = null,
                              bool? isConstant2Required = null)
        {
            Id = id;
            Code = code;
            Name = name;
            IsVariable1Required = isVariable1Required;
            IsVariable2Required = isVariable2Required;
            IsVariable3Required = isVariable3Required;
            IsVariable4Required = isVariable4Required;
            IsConstant1Required = isConstant1Required;
            IsConstant2Required = isConstant2Required;
        }
    }
}
