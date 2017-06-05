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
        public GUI_Course(string id,int ctdt,int hk)
        {
            InitializeComponent();
            if (id == "")
            {
                DBEntities db = new DBEntities();
                cboQuảnlí_giáoviên.DataSource = db.TaiKhoans.ToList();
                cboQuảnlí_giáoviên.ValueMember = "Id";
                cboQuảnlí_giáoviên.DisplayMember = "Ten";

                List<SP_MONHOC_VIEW_Result> arr = db.SP_MONHOC_VIEW(1).ToList();
                dtgwQuảnlí_môntiênquyết.DataSource = arr;
                dtgwQuảnlí_môntiênquyết.Columns[0].HeaderText = "Mã môn học";
                dtgwQuảnlí_môntiênquyết.Columns[1].HeaderText = "Mã Chương trình đào tạo";
                dtgwQuảnlí_môntiênquyết.Columns[2].HeaderText = "Tên môn học";
                dtgwQuảnlí_môntiênquyết.Columns[3].HeaderText = "Tên tiếng Anh";
                dtgwQuảnlí_môntiênquyết.Columns[4].HeaderText = "Loại kiến thức";
                dtgwQuảnlí_môntiênquyết.Columns[5].HeaderText = "Số tín chỉ";
                dtgwQuảnlí_môntiênquyết.Columns[6].HeaderText = "Học kỳ giảng dạy";
                dtgwQuảnlí_môntiênquyết.Columns[7].HeaderText = "Giảng viên phụ trách";
                dtgwQuảnlí_môntiênquyết.Columns[8].HeaderText = "Nội dung vắn tắt";
                dtgwQuảnlí_môntiênquyết.Columns[9].HeaderText = "Số giờ lý thuyết";
                dtgwQuảnlí_môntiênquyết.Columns[10].HeaderText = "Số giờ thực hành";

                DataGridViewCheckBoxColumn ck = new DataGridViewCheckBoxColumn();
                ck.HeaderText = "Môn tiên quyết";
                ck.Name = "Check";

                dtgwQuảnlí_môntiênquyết.Columns.Add(ck);
                
                if (hk == 4)
                {
                    List<string> lhk = new List<string> { "Học kì 1", "Học kì 2", "Học kì 3", "Học kì 4", "Học kì 5", "Học kì 6", "Học kì 7", "Học kì 8" };
                    cboQuảnlí_họckỳ.DataSource = lhk.ToList();
                }
                else
                {
                    List<string> lhk = new List<string> { "Học kì 1", "Học kì 2", "Học kì 3", "Học kì 4", "Học kì 5", "Học kì 6", "Học kì 7", "Học kì 8", "Học kì 9", "Học kì 10" };
                    cboQuảnlí_họckỳ.DataSource = lhk.ToList();
                }
            }
            else
            {
                idcourse = id;
                editconnect = new DBEntities();
                editobject = buscourse.getCourse(idcourse);

                txtQuảnlí_tên.Text = editobject.TenMonHoc;
                txtQuảnlí_tênES.Text = editobject.TenTiengAnh;
                txtQuảnlí_mã.Text = editobject.Id;
                txtQuảnlí_nộidungvắntắt.Text = editobject.NoiDungVanTat;
            }
            


            idctdt = ctdt;
        }
     

        private void cboQuảnlí_loạikt_1_SelectedValueChanged(object sender, EventArgs e)
        {          
            if (cboQuảnlí_loạikt_1.SelectedItem.ToString()=="Kiến thức giáo dục đại cương")
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
           
            if((cboQuảnlí_loạikt_2.SelectedItem.ToString() =="Khoa học xã hội")
                ||(cboQuảnlí_loạikt_2.SelectedItem.ToString() =="Nhân văn-Nghệ thuật")
                    ||(cboQuảnlí_loạikt_2.SelectedItem.ToString() =="Toán-Tin học-Khoa học tự nhiên-Công nghệ-Môi trường")
                        ||(cboQuảnlí_loạikt_2.SelectedItem.ToString() =="Kiến thức chuyên sâu của ngành chính")
                             || (cboQuảnlí_loạikt_2.SelectedItem.ToString() == "Kiến thức ngành thứ hai"))
                                    {
                                        List<string> arr = new List<string> {"Bắt buộc","Tự chọn"};
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
                    MessageBox.Show(rs,"Lỗi nhập dữ liệu");
                }
                    
            }
            
        }

        
    }
}
