using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_SEP_Team3.Educational_Program
{
    public partial class GUI_Course : Form
    {
        string idcourse = "";
        int idctdt;

        BUS_Course buscourse = new BUS_Course();

        DBEntities editconnect;
        MonHoc editobject;
        public GUI_Course(string id, int ctdt, int hk)
        {
            InitializeComponent();
            //Set up các componet khi form load lên
            DBEntities db = new DBEntities();
            cboQuảnlí_giáoviên.DataSource = db.TaiKhoans.ToList();
            cboQuảnlí_giáoviên.ValueMember = "Id";
            cboQuảnlí_giáoviên.DisplayMember = "Ten";

            List<SP_MONTIENQUYET_GET_Result> arr = db.SP_MONTIENQUYET_GET(ctdt,hk,id).ToList();
            dtgwQuảnlí_môntiênquyết.DataSource = arr;
            dtgwQuảnlí_môntiênquyết.Columns[0].HeaderText = "Mã môn học";
            dtgwQuảnlí_môntiênquyết.Columns[1].HeaderText = "Tên môn học";
            dtgwQuảnlí_môntiênquyết.Columns[2].HeaderText = "Tên tiếng Anh";
            dtgwQuảnlí_môntiênquyết.Columns[3].HeaderText = "Giảng viên phụ trách";
            if (id == "")
            {
                DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
                ck.HeaderText = "Môn tiên quyết";
                ck.Name = "Check";
                dtgwQuảnlí_môntiênquyết.Columns.Add(ck);
            }
                     
            dtgwQuảnlí_môntiênquyết.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            List<string> lhk = new List<string>(hk);
            for(int i=0;i< hk;i++){
                lhk.Add("Học kì "+(i+1));
            }
            cboQuảnlí_họckỳ.DataSource = lhk.ToList();
            
            //Set up khi form len là form edit
            if (id != "")
            {
                //Load thong tin cac truong
                idcourse = id;
                editconnect = new DBEntities();
                editobject = buscourse.getCourse(idcourse);

                txtQuảnlí_tên.Text = editobject.TenMonHoc;
                txtQuảnlí_tênES.Text = editobject.TenTiengAnh;
                txtQuảnlí_mã.Text = editobject.Id;
                cboQuảnlí_loạikt_1.SelectedIndex = int.Parse(editobject.LoaiKienThuc.ToString().Substring(0, 1)) - 1;
                cboQuảnlí_loạikt_2.SelectedIndex = int.Parse(editobject.LoaiKienThuc.ToString().Substring(1, 1)) - 1;
                int lkt3 = int.Parse(editobject.LoaiKienThuc.ToString().Substring(2, 1)) - 1;
                if (lkt3 >= 0)
                {
                    cboQuảnlí_loạikt_3.SelectedIndex = lkt3;
                }
                nQuảnlí_sốtínchỉ.Value = (decimal)editobject.SoTinChi;
                nQuảnlí_sốgiờlýthuyết.Value = (decimal)editobject.SoGioLyThuyet;
                nQuảnlí_sốgiờthựchành.Value = (decimal)editobject.SoGioThucHanh;
                cboQuảnlí_họckỳ.SelectedIndex = (int)editobject.HocKy - 1;
                cboQuảnlí_giáoviên.SelectedValue = editobject.GiangVienPhuTrach_Id;
                txtQuảnlí_nộidungvắntắt.Text = editobject.NoiDungVanTat;

                //Load mon tien quyet
                dtgwQuảnlí_môntiênquyết.DataSource = buscourse.getCourse_MTQ(ctdt,hk,id);

                
                List<Montienquyet_dtgv> alst = new List<Montienquyet_dtgv>();
                for (int i = 0; i < dtgwQuảnlí_môntiênquyết.Rows.Count; i++)
                {
                    string aid = (string)dtgwQuảnlí_môntiênquyết.Rows[i].Cells[0].Value;
                    string at = (string)dtgwQuảnlí_môntiênquyết.Rows[i].Cells[1].Value;
                    string ates = (string)dtgwQuảnlí_môntiênquyết.Rows[i].Cells[2].Value;
                    string agv = (string)dtgwQuảnlí_môntiênquyết.Rows[i].Cells[3].Value;
                    bool ac = (bool)dtgwQuảnlí_môntiênquyết.Rows[i].Cells[4].Value;
                    alst.Add(new Montienquyet_dtgv(aid,at,ates,agv,ac));
                }
                dtgwQuảnlí_môntiênquyết.DataSource = null;
                dtgwQuảnlí_môntiênquyết.Columns.Add(new DataGridViewTextBoxColumn());
                dtgwQuảnlí_môntiênquyết.Columns.Add(new DataGridViewTextBoxColumn());
                dtgwQuảnlí_môntiênquyết.Columns.Add(new DataGridViewTextBoxColumn());
                dtgwQuảnlí_môntiênquyết.Columns.Add(new DataGridViewTextBoxColumn());
                dtgwQuảnlí_môntiênquyết.Columns.Add(new DataGridViewCheckBoxColumn());
                foreach (Montienquyet_dtgv i in alst)
                {
                    
                    dtgwQuảnlí_môntiênquyết.Rows.Add(i.id,i.name,i.namees,i.teacher,i.check);
                }
                dtgwQuảnlí_môntiênquyết.Columns[0].HeaderText = "Mã môn học";
                dtgwQuảnlí_môntiênquyết.Columns[0].ReadOnly = true;
                dtgwQuảnlí_môntiênquyết.Columns[1].HeaderText = "Tên môn học";
                dtgwQuảnlí_môntiênquyết.Columns[1].ReadOnly = true;
                dtgwQuảnlí_môntiênquyết.Columns[2].HeaderText = "Tên tiếng Anh";
                dtgwQuảnlí_môntiênquyết.Columns[2].ReadOnly = true;
                dtgwQuảnlí_môntiênquyết.Columns[3].HeaderText = "Giảng viên phụ trách";
                dtgwQuảnlí_môntiênquyết.Columns[3].ReadOnly = true;
                dtgwQuảnlí_môntiênquyết.Columns[4].HeaderText = "Môn tiên quyết";

                btnQuảnlí_lưu.Text = "Cập nhật";
                txtQuảnlí_mã.ReadOnly = true;
            }

            idctdt = ctdt;
        }


        private void cboQuảnlí_loạikt_1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cboQuảnlí_loạikt_1.SelectedItem.ToString() == "Kiến thức giáo dục đại cương")
            {
                List<string> arr = new List<string> { "Lý luận chính trị", "Khoa học xã hội", 
                    "Nhân văn-Nghệ thuật", "Ngoại ngữ", "Toán-Tin học-Khoa học tự nhiên-Công nghệ-Môi trường", 
                        "Giáo dục thể chất", "Giáo dục Quốc Phòng- an ninh" };
                cboQuảnlí_loạikt_2.DataSource = arr.ToList();
            }
            if (cboQuảnlí_loạikt_1.SelectedItem.ToString() == "Kiến thức giáo dục chuyên nghiệp")
            {
                List<string> arr = new List<string> { "Kiến thức cơ sở", "Kiến thức ngành chính", 
                    "Kiến thức chung của ngành chính", "Kiến thức chuyên sâu của ngành chính", 
                        "Kiến thức ngành thứ hai", "Kiến thức bổ trợ tự do", "Thực tập tốt nghiệp và làm khóa luận" };
                cboQuảnlí_loạikt_2.DataSource = arr.ToList();
            }
        }

        private void cboQuảnlí_loạikt_2_SelectedValueChanged(object sender, EventArgs e)
        {

            if ((cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Khoa học xã hội")
                || (cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Nhân văn-Nghệ thuật")
                    || (cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Toán-Tin học-Khoa học tự nhiên-Công nghệ-Môi trường")
                        || (cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Kiến thức chuyên sâu của ngành chính")
                             || (cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Kiến thức ngành thứ hai"))
            {
                List<string> arr = new List<string> { "Bắt buộc", "Tự chọn" };
                cboQuảnlí_loạikt_3.DataSource = arr.ToList();
            }
            else
            {
                List<string> arr = new List<string> { "" };
                cboQuảnlí_loạikt_3.DataSource = arr.ToList();
            }
        }

        private void btnQuảnlí_lưu_Click(object sender, EventArgs e)
        {
            if (btnQuảnlí_lưu.Text == "Lưu")
            {
                string rs = buscourse.addCourse(idctdt, txtQuảnlí_tên, txtQuảnlí_tênES, txtQuảnlí_mã,
                                    cboQuảnlí_loạikt_1, cboQuảnlí_loạikt_2, cboQuảnlí_loạikt_3,
                                        nQuảnlí_sốtínchỉ, nQuảnlí_sốgiờlýthuyết, nQuảnlí_sốgiờthựchành,
                                             cboQuảnlí_họckỳ, cboQuảnlí_giáoviên, txtQuảnlí_nộidungvắntắt, dtgwQuảnlí_môntiênquyết);

                if (rs == "")
                {
                    MessageBox.Show("Thêm mới môn học thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(rs, "Lỗi nhập dữ liệu");
                }

            }
            if (btnQuảnlí_lưu.Text == "Cập nhật")
            {
                string rs = buscourse.editCourse(idctdt,idcourse, txtQuảnlí_tên, txtQuảnlí_tênES, txtQuảnlí_mã,
                                    cboQuảnlí_loạikt_1, cboQuảnlí_loạikt_2, cboQuảnlí_loạikt_3,
                                        nQuảnlí_sốtínchỉ, nQuảnlí_sốgiờlýthuyết, nQuảnlí_sốgiờthựchành,
                                             cboQuảnlí_họckỳ, cboQuảnlí_giáoviên, txtQuảnlí_nộidungvắntắt, dtgwQuảnlí_môntiênquyết);

                if (rs == "\nMã môn học đã tồn tại")
                {
                    MessageBox.Show("Cập nhật môn học thành công");
                    this.Close();
                }
                else
                {
                    MessageBox.Show(rs, "Lỗi nhập dữ liệu");
                }
            }

        }


    }
}
