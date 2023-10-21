using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BOnlineStore.Shared
{
    /// <summary>
    /// Formül türü enum sınıfı
    /// </summary>
    public static class FormulaSortEnum
    {
        /// <summary>
        /// Formül türü enumu
        /// </summary>
        public enum FormulaSort
        {
            /// <summary>
            /// Her ikisinde çıkan formül
            /// </summary>
            Both,
            /// <summary>
            /// İmalatta çıkan formül
            /// </summary>
            Production,
            /// <summary>
            /// Maliyette çıkan formül
            /// </summary>
            Cost
        }
    }
}
