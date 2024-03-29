﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;


namespace Kitaplık
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        OleDbConnection baglanti= new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\emre\\Desktop\\Kitaplıkk.accdb");


        void listele()
        { 
            DataTable dt = new DataTable();
            OleDbDataAdapter da= new OleDbDataAdapter("Select * From Kitaplarr",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource= dt;
         
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        string durum = " ";
        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("insert into Kitaplarr(KitapAd,Yazar,Tur,Sayfa,Durum) values (@p1,@p2,@p3,@p4,@p5)", baglanti);
            komut.Parameters.AddWithValue("@p1", TxtKitapAdı.Text);
            komut.Parameters.AddWithValue("@p2", TxtYazar.Text);
            komut.Parameters.AddWithValue("@p3", TxtSayfaSayısı.Text);
            komut.Parameters.AddWithValue("@p4", CmbTur.Text);
            komut.Parameters.AddWithValue("@p5", durum);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Sisteme Kaydedildi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            durum = "0";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            durum = "1";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            TxtKitapid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            TxtKitapAdı.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            TxtSayfaSayısı.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString()=="True")
            {
                radioButton2.Checked= true;
            }
            else
            { 
                radioButton1.Checked= true;
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("Delete From Kitaplarr where kitapid=@p1", baglanti);
            cmd.Parameters.AddWithValue("@p1", TxtKitapid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kitap Listeden Silindi","Bilgi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand cmd = new OleDbCommand("Update Kitaplarr set KitapAd=@p1,Yazar=@p2,Tur=@p3,Sayfa=@p4,Durum=@p5 where Kitapid=@p6", baglanti);
            cmd.Parameters.AddWithValue("@p1",TxtKitapAdı.Text);
            cmd.Parameters.AddWithValue("@p2", TxtYazar.Text);
            cmd.Parameters.AddWithValue("@p3", CmbTur.Text);
            cmd.Parameters.AddWithValue("@p4",TxtSayfaSayısı.Text);
            if (radioButton1.Checked ==true)
            {
                cmd.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true)
            {
                cmd.Parameters.AddWithValue("@p5", durum);
            }
            cmd.Parameters.AddWithValue("@p6", TxtKitapid.Text);
            cmd.ExecuteNonQuery();
            baglanti.Close();
            listele();
            MessageBox.Show("Kayıt Güncellendi","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);



        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand cmd1 = new OleDbCommand("Select * From Kitaplarr where KitapAd=@p1", baglanti);
            cmd1.Parameters.AddWithValue("@p1", TxtBul.Text);
            DataTable dt= new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView1.DataSource= dt;
            baglanti.Close();
        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            OleDbCommand cmd1 = new OleDbCommand("Select * From Kitaplarr where KitapAd like '%" + TxtBul.Text +  "%'", baglanti);
            cmd1.Parameters.AddWithValue("@p1", TxtBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();

        }
    }
}



//Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\emre\Desktop\Kitaplıkk.accdb