//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Prototype_SEP_Team3
{
    using System;
    using System.Collections.Generic;
    
    public partial class MaTran_CDRMH_CDRCTDT
    {
        public int Id { get; set; }
        public int DCCT_Id { get; set; }
        public int CDRMH_Id { get; set; }
        public int CDRCTDT_Id { get; set; }
        public bool Mapped { get; set; }
    
        public virtual ChuanDauRaMonHoc ChuanDauRaMonHoc { get; set; }
        public virtual DeCuongChiTiet DeCuongChiTiet { get; set; }
        public virtual MucTieuDaoTao MucTieuDaoTao { get; set; }
    }
}
