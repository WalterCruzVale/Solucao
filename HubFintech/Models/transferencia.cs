//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HubFintech.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class transferencia
    {
        public int IDTransferencia { get; set; }
        public Nullable<int> IDContaOrigem { get; set; }
        public Nullable<int> IDContaDestino { get; set; }
        public Nullable<decimal> ValorTransferencia { get; set; }
    
        public virtual Conta Conta { get; set; }
        public virtual Conta Conta1 { get; set; }
    }
}
