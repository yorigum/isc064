using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ISC064.LibControls
{
    public class Bind
    {
        public static string TipeApproval(byte x)
        {
            string z = "";
            switch (x)
            {
                case 1: z = "Pengalihan Hak"; break;
                case 2: z = "Pindah Unit"; break;
                case 3: z = "Pembatalan Kontrak"; break;
                case 4: z = "Diskon"; break;
                case 5: z = "Adjustment Kontrak"; break;
                case 6: z = "Reschedule Tagihan"; break;
                case 7: z = "Customize Tagihan"; break;
            }
            return z;
        }
        public static string KolomCustomer(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "alamat1"; break;
                case 2: a = "alamat2"; break;
                case 3: a = "alamat3"; break;
                case 4: a = "alamat4"; break;
                case 5: a = "alamat5"; break;
                case 6: a = "email"; break;
                case 7: a = "emailhub"; break;
                case 8: a = "hphub"; break;
                case 9: a = "kantor1"; break;
                case 10: a = "kantor2"; break;
                case 11: a = "kantor3"; break;
                case 12: a = "kantor4"; break;
                case 13: a = "kantor5"; break;
                case 14: a = "kodepos1"; break;
                case 15: a = "ktp1"; break;
                case 16: a = "ktp2"; break;
                case 17: a = "ktp3"; break;
                case 18: a = "ktp4"; break;
                case 19: a = "ktp5"; break;
                case 20: a = "Nama"; break;
                case 21: a = "namahub"; break;
                case 22: a = "namanpwp"; break;
                case 23: a = "nofax"; break;
                case 24: a = "nohp"; break;
                case 25: a = "nohp2"; break;
                case 26: a = "noktp"; break;
                case 27: a = "notelp"; break;
                case 28: a = "npwp"; break;
                case 29: a = "npwp1"; break;
                case 30: a = "npwp2"; break;
                case 31: a = "npwp3"; break;
                case 32: a = "npwp4"; break;
                case 33: a = "npwp5"; break;
                case 34: a = "pekerjaan"; break;
                case 35: a = "tempatlahir"; break;
                case 36: a = "tglktp"; break;
                case 37: a = "tgllahir"; break;
                case 38: a = "tlpkantor"; break;
            }
            return a;
        }
        public static string KetCustomer(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "Alamat surat menyurat."; break;
                case 2: a = "RT/ RW surat menyurat."; break;
                case 3: a = "Kelurahan surat menyurat."; break;
                case 4: a = "Kecamatan surat menyurat."; break;
                case 5: a = "Kotamadya surat menyurat."; break;
                case 6: a = "Email customer."; break;
                case 7: a = "Email orang terdekat."; break;
                case 8: a = "HP orang terdekat."; break;
                case 9: a = "Alamat kantor."; break;
                case 10: a = "RT/ RW kantor."; break;
                case 11: a = "Kelurahan kantor."; break;
                case 12: a = "Kecamatan kantor."; break;
                case 13: a = "Kotamadya kantor."; break;
                case 14: a = "Kode pos."; break;
                case 15: a = "Alamat customer."; break;
                case 16: a = "RT/ RW."; break;
                case 17: a = "Kecamatan."; break;
                case 18: a = "Kotamadya/ Kabupaten."; break;
                case 19: a = "Kelurahan."; break;
                case 20: a = "Nama Customer"; break;
                case 21: a = "Nama orang terdekat."; break;
                case 22: a = "Nama npwp."; break;
                case 23: a = "Nomor fax customer."; break;
                case 24: a = "Nomor handphone customer."; break;
                case 25: a = "Nomor handphone (kedua) customer."; break;
                case 26: a = "No. KTP Customer."; break;
                case 27: a = "Nomor telepon customer."; break;
                case 28: a = "Nomor npwp."; break;
                case 29: a = "Alamat NPWP."; break;
                case 30: a = "RT/ RW NPWP."; break;
                case 31: a = "Kelurahan NPWP."; break;
                case 32: a = "Kecamatan NPWP."; break;
                case 33: a = "Kotamadya NPWP."; break;
                case 34: a = "Nama pekerjaan."; break;
                case 35: a = "Tempat Lahir."; break;
                case 36: a = "Tanggal berakhir KTP."; break;
                case 37: a = "Tanggal Lahir."; break;
                case 38: a = "Nomor telepon kantor."; break;
            }
            return a;
        }
        public static string TipeDataCustomer(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "0"; break;
                case 2: a = "0"; break;
                case 3: a = "0"; break;
                case 4: a = "0"; break;
                case 5: a = "0"; break;
                case 6: a = "0"; break;
                case 7: a = "0"; break;
                case 8: a = "0"; break;
                case 9: a = "0"; break;
                case 10: a = "0"; break;
                case 11: a = "0"; break;
                case 12: a = "0"; break;
                case 13: a = "0"; break;
                case 14: a = "0"; break;
                case 15: a = "0"; break;
                case 16: a = "0"; break;
                case 17: a = "0"; break;
                case 18: a = "0"; break;
                case 19: a = "0"; break;
                case 20: a = "0"; break;
                case 21: a = "0"; break;
                case 22: a = "0"; break;
                case 23: a = "0"; break;
                case 24: a = "0"; break;
                case 25: a = "0"; break;
                case 26: a = "0"; break;
                case 27: a = "0"; break;
                case 28: a = "0"; break;
                case 29: a = "0"; break;
                case 30: a = "0"; break;
                case 31: a = "0"; break;
                case 32: a = "0"; break;
                case 33: a = "0"; break;
                case 34: a = "0"; break;
                case 35: a = "0"; break;
                case 36: a = "1"; break;
                case 37: a = "0"; break;
                case 38: a = "0"; break;
            }
            return a;
        }
        public static string KolomRekening(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "acc"; break;
                case 2: a = "atasnama"; break;
                case 3: a = "bank"; break;
                case 4: a = "cabang1"; break;
                case 5: a = "rekening"; break;
                case 6: a = "saldoawal"; break;
                case 7: a = "subid"; break;
            }
            return a;
        }
        public static string KetRekening(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "Nomor account."; break;
                case 2: a = "Atas nama."; break;
                case 3: a = "Nama bank."; break;
                case 4: a = "Cabang."; break;
                case 5: a = "Rekening."; break;
                case 6: a = "Saldo awal."; break;
                case 7: a = "Sub ID."; break;
            }
            return a;
        }
        public static string TipeDataRekening(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "0"; break;
                case 2: a = "0"; break;
                case 3: a = "0"; break;
                case 4: a = "0"; break;
                case 5: a = "0"; break;
                case 6: a = "2"; break;
                case 7: a = "0"; break;
            }
            return a;
        }
        public static string KolomSales(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "alamat"; break;
                case 2: a = "atasnama1"; break;
                case 3: a = "email1"; break;
                case 4: a = "hp"; break;
                case 5: a = "kodesls"; break;
                case 6: a = "nama"; break;
                case 7: a = "npwp"; break;
                case 8: a = "rek"; break;
                case 9: a = "rekbank1"; break;
                case 10: a = "telp"; break;
                case 11: a = "wa"; break;
            }
            return a;
        }
        public static string KetSales(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "Alamat sales."; break;
                case 2: a = "Bank atas nama."; break;
                case 3: a = "Email sales."; break;
                case 4: a = "Nomor hp sales."; break;
                case 5: a = "Kode sales."; break;
                case 6: a = "Nama sales."; break;
                case 7: a = "Nomor NPWP sales."; break;
                case 8: a = "Nomor rekening sales."; break;
                case 9: a = "Nama bank."; break;
                case 10: a = "Nomor telepon sales."; break;
                case 11: a = "Nomor wa sales."; break;
            }
            return a;
        }
        public static string TipeDataSales(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "0"; break;
                case 2: a = "0"; break;
                case 3: a = "0"; break;
                case 4: a = "0"; break;
                case 5: a = "0"; break;
                case 6: a = "0"; break;
                case 7: a = "0"; break;
                case 8: a = "0"; break;
                case 9: a = "0"; break;
                case 10: a = "0"; break;
                case 11: a = "0"; break;
            }
            return a;
        }
        public static string KolomUnit(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "lantai"; break;
                case 2: a = "nomor"; break;
                case 3: a = "luassg"; break;
                case 4: a = "luaslebih"; break;
                case 5: a = "luasnett"; break;
            }
            return a;
        }
        public static string KetUnit(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "Blok."; break;
                case 2: a = "Nomor."; break;
                case 3: a = "Luas Tanah."; break;
                case 4: a = "Luas Lebih Tanah."; break;
                case 5: a = "Luas Bangunan."; break;
            }
            return a;
        }
        public static string TipeDataUnit(byte x)
        {
            string a = "";
            switch (x)
            {
                case 1: a = "0"; break;
                case 2: a = "2"; break;
                case 3: a = "2"; break;
                case 4: a = "2"; break;
                case 5: a = "2"; break;
            }
            return a;
        }

    }
}
