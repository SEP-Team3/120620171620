using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prototype_SEP_Team3.Educational_Program
{
    class BUS_EP
    {
        //kiểm tra thời gian đào tạo
        public bool getThoigiandaotao(double newtime,int ctdt)
        {
            DBEntities db = new DBEntities();
            ThongTinChung_CTDT check = db.ThongTinChung_CTDT.Single(x=>x.ChuongTrinhDaoTao_Id==ctdt);
            if (newtime == check.ThoiGianDaoTao)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //kiem tra khi thời gian đào tạo thay đổi
        public string checkThoigiandaotao(double newtime, int ctdt)
        {
            DBEntities db = new DBEntities();
            ThongTinChung_CTDT find = db.ThongTinChung_CTDT.Single(x => x.ChuongTrinhDaoTao_Id == ctdt);
            List<MonHoc> findcourse = db.MonHocs.Where(x => x.ChuongTrinhDaoTao_Id == ctdt).ToList();
            if ((newtime != find.ThoiGianDaoTao)&&(findcourse.Count>0))
            {
                return "false";
            }
            else
            {
                return "true";
            }
        }

        // xu li khi thoi gian thay doi
        public void handleThoigiandaotao(int ctdt)
        {
            DBEntities db = new DBEntities();
            db.SP_THOIGIANDAOTAO_HANDLE(ctdt);
        }

        // ve table
        public void drawTable(DataGridView ilst)
        {
            //Nội dung chương trình
            TableLayoutPanel NDCTpanel = new TableLayoutPanel();
            NDCTpanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            NDCTpanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            NDCTpanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            NDCTpanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            NDCTpanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            NDCTpanel.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            NDCTpanel.Controls.Add(new Label() { Text = "STT", Anchor = AnchorStyles.None }, 0, 0);
            NDCTpanel.Controls.Add(new Label() { Text = "MÃ MH", Anchor = AnchorStyles.None }, 1, 0);
            NDCTpanel.Controls.Add(new Label() { Text = "Môn học", Anchor = AnchorStyles.None }, 2, 0);
            NDCTpanel.Controls.Add(new Label() { Text = "TC", Anchor = AnchorStyles.None }, 3, 0);
            NDCTpanel.Controls.Add(new Label() { Text = "Học kỳ", Anchor = AnchorStyles.None }, 4, 0);
            Label lblktdc = new Label();
            lblktdc.Text = "Kiến thức giáo dục đại cương";
            NDCTpanel.Controls.Add(lblktdc, 0, 1);
            NDCTpanel.SetRowSpan(lblktdc, 3);
            Label lblktdc1 = new Label();
            lblktdc1.Text = "Lý luận Mac-Lenin và tư tưởng Hồ Chí Minh";
            NDCTpanel.Controls.Add(lblktdc1, 1, 2);
            NDCTpanel.SetRowSpan(lblktdc1, 2);
            int stt = 1;
            int tc = 0;
            int row = 3;
            for (int i = 0; i < ilst.RowCount; i++)
            {
                int check1 = int.Parse(ilst.Rows[i].Cells[5].Value.ToString().Substring(0, 1));
                int check2 = int.Parse(ilst.Rows[i].Cells[5].Value.ToString().Substring(1, 1));
                if ((check1 == 1) && (check2 == 1))
                {
                    NDCTpanel.Controls.Add(new TextBox() { Text = stt.ToString(),ReadOnly=true,BorderStyle=BorderStyle.None },0,row);
                    NDCTpanel.Controls.Add(new TextBox() { Text = ilst.Rows[i].Cells[1].Value.ToString(), ReadOnly = true, BorderStyle = BorderStyle.None }, 1, row);
                    NDCTpanel.Controls.Add(new TextBox() { Text = ilst.Rows[i].Cells[3].Value.ToString(), ReadOnly = true, BorderStyle = BorderStyle.None }, 2, row);
                    NDCTpanel.Controls.Add(new TextBox() { Text = ilst.Rows[i].Cells[6].Value.ToString(), ReadOnly = true, BorderStyle = BorderStyle.None }, 3, row);
                    NDCTpanel.Controls.Add(new TextBox() { Text = ilst.Rows[i].Cells[7].Value.ToString(), ReadOnly = true, BorderStyle = BorderStyle.None }, 4, row);
                    stt++;
                    tc += int.Parse(ilst.Rows[i].Cells[6].Value.ToString());
                    row++;
                }
            }
            Label lblktdctc = new Label();
            lblktdctc.Text = tc.ToString();
            NDCTpanel.Controls.Add(lblktdctc, 3, 2);
            NDCTpanel.SetRowSpan(lblktdc, 2);
            

            

        }
        
        
    }
}
