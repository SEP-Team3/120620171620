using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_SEP_Team3.Educational_Program
{
    class BUS_Course
    {

        //Check lỗi input trước khi thêm Môn học
        public string checkCourse(TextBox txtQuảnlí_tên, TextBox txtQuảnlí_tênES, TextBox txtQuảnlí_mã,
                                    ComboBox cboQuảnlí_loạikt_1, ComboBox cboQuảnlí_loạikt_2, ComboBox cboQuảnlí_loạikt_3,
                                            NumericUpDown nQuảnlí_sốtínchỉ, NumericUpDown nQuảnlí_sốgiờlýthuyết, NumericUpDown nQuảnlí_sốgiờthựchành,
                                                  ComboBox cboQuảnlí_họckỳ, ComboBox cboQuảnlí_giáoviên, TextBox txtQuảnlí_nộidungvắntắt)
        {

            string result = "";
            if (txtQuảnlí_tên.Text == "")
            {
                result += "\nTên không được để trống";
            }
            if (txtQuảnlí_tênES.Text == "")
            {
                result += "\nTên tiếng Anh không được để trống";
            }
            if (txtQuảnlí_mã.Text == "")
            {
                result += "\nMã môn học không được để trống";
            }
            if (txtQuảnlí_nộidungvắntắt.Text == "")
            {
                result += "\nNội dung vắn tắt của môn học không được để trống";
            }
            if ((cboQuảnlí_loạikt_1.Text == "") || (cboQuảnlí_loạikt_2.Text == ""))
            {
                result += "\nLoại kiến thức chưa đầy đủ";
            }
            if (nQuảnlí_sốtínchỉ.Value == 0)
            {
                result += "\nSố tín chỉ phải lớn hơn không";
            }
            if (nQuảnlí_sốgiờlýthuyết.Value == 0)
            {
                result += "\nSố giờ lý thuyết phải lớn hơn không";
            }
            if (nQuảnlí_sốtínchỉ.Value != (nQuảnlí_sốgiờlýthuyết.Value / 15 + nQuảnlí_sốgiờthựchành.Value / 30))
            {
                result += "\nSố giờ lý thuyết, Số giờ thực hành chưa phù hợp với Số tín chỉ";
            }

            return result;


        }

        //Thêm mới môn học vào bảng Môn học và Môn tiên quyết vào bàng Montienquyet
        public string addCourse(int idctdt, TextBox txtQuảnlí_tên, TextBox txtQuảnlí_tênES, TextBox txtQuảnlí_mã,
                                    ComboBox cboQuảnlí_loạikt_1, ComboBox cboQuảnlí_loạikt_2, ComboBox cboQuảnlí_loạikt_3,
                                            NumericUpDown nQuảnlí_sốtínchỉ, NumericUpDown nQuảnlí_sốgiờlýthuyết, NumericUpDown nQuảnlí_sốgiờthựchành,
                                                  ComboBox cboQuảnlí_họckỳ, ComboBox cboQuảnlí_giáoviên, TextBox txtQuảnlí_nộidungvắntắt, DataGridView dtgwQuảnlí_môntiênquyết)
        {
            string checkrs = checkCourse(txtQuảnlí_tên, txtQuảnlí_tênES, txtQuảnlí_mã,
                                cboQuảnlí_loạikt_1, cboQuảnlí_loạikt_2, cboQuảnlí_loạikt_3,
                                    nQuảnlí_sốtínchỉ, nQuảnlí_sốgiờlýthuyết, nQuảnlí_sốgiờthựchành,
                                       cboQuảnlí_họckỳ, cboQuảnlí_giáoviên, txtQuảnlí_nộidungvắntắt);
            if (checkrs == "")
            {
                string ten = txtQuảnlí_tên.Text;
                string tenes = txtQuảnlí_tênES.Text;
                string mamh = txtQuảnlí_mã.Text;
                int lkt = 0;
                if (cboQuảnlí_loạikt_3.Text == "")
                {
                    lkt = (cboQuảnlí_loạikt_1.SelectedIndex + 1) * 100 + (cboQuảnlí_loạikt_2.SelectedIndex + 1) * 10;
                }
                else
                {
                    lkt = (cboQuảnlí_loạikt_1.SelectedIndex + 1) * 100 + (cboQuảnlí_loạikt_2.SelectedIndex + 1) * 10 + (cboQuảnlí_loạikt_3.SelectedIndex + 1);
                }

                int stc = (int)nQuảnlí_sốtínchỉ.Value;
                int lt = (int)nQuảnlí_sốgiờlýthuyết.Value;
                int th = (int)nQuảnlí_sốgiờthựchành.Value;
                int hk = cboQuảnlí_họckỳ.SelectedIndex + 1;
                int giaovienid = (int)cboQuảnlí_giáoviên.SelectedValue;
                string ndvt = txtQuảnlí_nộidungvắntắt.Text;


                DBEntities db = new DBEntities();

                MonHoc add = new MonHoc();

                add.ChuongTrinhDaoTao_Id = idctdt;
                add.TenMonHoc = ten;
                add.TenTiengAnh = tenes;
                add.Id = mamh;
                add.LoaiKienThuc = lkt;
                add.SoTinChi = stc;
                add.SoGioLyThuyet = lt;
                add.SoGioThucHanh = th;
                add.HocKy = hk;
                add.GiangVienPhuTrach_Id = giaovienid;
                add.NoiDungVanTat = ndvt;
                db.MonHocs.Add(add);
                db.SaveChanges();



                for (int i = 0; i < dtgwQuảnlí_môntiênquyết.RowCount; i++)
                {
                    if (dtgwQuảnlí_môntiênquyết.Rows[i].Cells["Check"].Value != null)
                    {
                        if ((bool)dtgwQuảnlí_môntiênquyết.Rows[i].Cells["Check"].Value == true)
                        {
                            MonTienQuyet mtq = new MonTienQuyet();
                            mtq.MonHoc_Id = mamh;
                            mtq.Montienquyet_Id = (string)dtgwQuảnlí_môntiênquyết.Rows[i].Cells["Id"].Value;
                            db.MonTienQuyets.Add(mtq);
                            db.SaveChanges();
                        }
                    }
                }

                return checkrs;
            }
            else
            {
                return checkrs;
            }

        }

        //Lấy Course theo id
        public MonHoc getCourse(string id)
        {
            DBEntities db = new DBEntities();
            return db.MonHocs.Single(x => x.Id == id);
        }

        //Lấy môn tiên quyết của course
        public List<MonTienQuyet> getCourse_MTQ(string id)
        {
            DBEntities db = new DBEntities();
            return db.MonTienQuyets.Where(x => x.MonHoc_Id == id).ToList();
        }
    }
}
