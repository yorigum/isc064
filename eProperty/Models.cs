
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace ISC064.Models
{

    public class Data
    {

        public static List<Tagihan> ListTagihan(string NoKontrak)
        {


            List<Tagihan> x = new List<Tagihan>();
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "Marketingjual;Select * from ms_tagihan where NoKontrak='" + NoKontrak + "'");
            foreach (DataRow r in rs.Rows)
            {

                List<Pelunasan> b = new List<Pelunasan>();
                DataTable rsa = Db.Rs("use " + Mi.DbPrefix + "Marketingjual;Select * from ms_pelunasan where NoKontrak='" + NoKontrak + "' and NoTagihan='" + r["NoUrut"] + "'");
                foreach (DataRow ra in rsa.Rows)
                {
                    List<Kwitansi> c = new List<Kwitansi>();
                    DataTable rsb = Db.Rs("use " + Mi.DbPrefix + "Financear;Select * from ms_TTS where NOTTS='" + ra["NoTTS"] + "' ");
                    foreach (DataRow rb in rsb.Rows)
                    {

                        c.Add(new Kwitansi
                        {
                            NoTTS = rb["NoTTS"].ToString(),
                            NoBKM = rb["NoBKM"].ToString(),
                            NoKwitansi = rb["NoBKM2"].ToString(),
                            CaraBayar = (string)rb["carabayar"],
                            Keterangan = (string)rb["Ket"],
                            NilaiKwitansi = (decimal)rb["Total"],
                            Status = (string)rb["Status"],
                            TglTTS = (DateTime)rb["TglTTS"],
                            User = (string)rb["UserID"],
                            TglBKM = Cf.Day(rb["TglBKM"])
                        });
                    }

                    b.Add(new Pelunasan
                    {
                        NoKontrak = NoKontrak,
                        CaraBayar = (string)ra["CaraBayar"],
                        NilaiPelunasan = (decimal)ra["NilaiPelunasan"],
                        Keterangan = (string)ra["ket"],
                        NoBKM = ra["NoBKM"].ToString(),
                        NoMEMO = ra["NoMEMO"].ToString(),
                        NoTagihan = (int)ra["NoTagihan"],
                        NoTTS = ra["NoTTS"].ToString(),
                        SN = (int)ra["NoUrut"],
                        SudahCair = (bool)ra["SudahCair"],
                        TglPelunasan = (DateTime)ra["TglPelunasan"],
                        ListKwitansi = c,
                        Alokasi = ((string)ra["CaraBayar"] == "AL") ? (decimal)ra["NilaiPelunasan"] : 0
                    });
                }

                x.Add(new Tagihan
                {
                    NoKontrak = NoKontrak,
                    NamaTagihan = (string)r["NamaTagihan"],
                    NoTagihan = (int)r["NoUrut"],
                    NilaiTagihan = (decimal)r["NilaiTagihan"],
                    TglJT = (DateTime)r["TglJT"],
                    Tipe = (string)r["Tipe"],
                    FlagPJT = (bool)r["FlagPJT"],
                    NilaiDenda = (decimal)r["Denda"],
                    NilaiDendaReal = (decimal)r["DendaReal"],
                    isKPR = (bool)r["KPR"],
                    Akad = (bool)r["Akad"],
                    PutihDenda = (bool)r["PutihDenda"],
                    NilaiPutihDenda = (decimal)r["NilaiPutihDenda"],
                    ListPelunasan = b,
                    SisaTagihan = ((decimal)r["NilaiTagihan"]) - b.Sum(p => p.NilaiPelunasan) > 0 ? ((decimal)r["NilaiTagihan"]) - b.Sum(p => p.NilaiPelunasan) : 0,
                    LebihBayar = b.Sum(p => p.NilaiPelunasan) - ((decimal)r["NilaiTagihan"]) > 0 ? b.Sum(p => p.NilaiPelunasan) - ((decimal)r["NilaiTagihan"]) : 0

                });
            }
            return x;

        }

        public static Kontrak Kontrak(object NoKontrak)
        {
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual; Select * from MS_Kontrak where NoKontrak='" + NoKontrak + "'");
            if (rs.Rows.Count > 0)
            {
                Kontrak x = new Kontrak();
                var r = rs.Rows[0];

                x.NoKontrak = NoKontrak.ToString();
                x.Agent = Agent(r["NoAgent"]); ;
                x.NoAgent = r["NoAgent"].ToString(); ;
                x.NoCustomer = r["NoCustomer"].ToString(); ;
                x.Unit = Unit(r["NoStock"]);
                x.SkemaCaraBayar = Skema(r["RefSkema"]);
                x.Customer = Customer(r["NoCustomer"]);
                x.DiskonPersen = r["DiskonPersen"].ToString();
                x.DiskonTambahan = (decimal)r["DiskonTambahan"];
                x.DiskonRupiah = (decimal)r["DiskonRupiah"];
                x.BungaNominal = r["BungaNominal"].Equals(System.DBNull.Value) ? 0 : (decimal)r["BungaNominal"];
                x.BungaPersen = (string)r["BungaPersen"];
                x.Gross = (decimal)r["Gross"];
                x.NilaiDPP = (decimal)r["NilaiDPP"];
                x.NilaiKlaim = (decimal)r["NilaiKlaim"];
                x.NilaiPPN = (decimal)r["NilaiPPN"];
                x.NilaiKontrak = (decimal)r["NilaiKontrak"];
                x.NilaiRealisasiKPR = (decimal)r["NilaiRealisasiKPR"];
                x.outbalance = (decimal)r["outBalance"];
                x.PersenLunas = (decimal)r["persenLunas"];
                x.TglKontrak = (DateTime)r["TglKontrak"];
                x.Status = r["Status"].ToString();
                x.FlagProsesbatal = (int)r["FlagProsesBatal"];
                x.NoST = r["NoST"].ToString();
                x.NoVA = r["NoVA"].ToString();
                if (!(r["TglST"] is DBNull)) x.TglBAST = (DateTime)r["TglST"];
                if (!(r["TglPPJB"] is DBNull)) x.TglPPJB = (DateTime)r["TglPPJB"];
                if (!(r["TglBatal"] is DBNull)) x.TglPPJB = (DateTime)r["TglBatal"];
                return x;
            }
            return null;
        }
        public static List<Kontrak> Kontraks
        {
            get
            {               
                return (from DataRow r in Db.Rs("use " + Mi.DbPrefix + "marketingjual; Select * from MS_Kontrak").Rows
                        select new Kontrak
                        {
                            NoKontrak = r["NoKontrak"].ToString(),
                            Agent = Agent(r["NoAgent"]),
                            NoAgent = r["NoAgent"].ToString(),
                            NoCustomer = r["NoCustomer"].ToString(),
                            Unit = Unit(r["NoStock"]),
                            SkemaCaraBayar = Skema(r["RefSkema"]),
                            Customer = Customer(r["NoCustomer"]),
                            DiskonPersen = r["DiskonPersen"].ToString(),
                            DiskonTambahan = (decimal)r["DiskonTambahan"],
                            DiskonRupiah = (decimal)r["DiskonRupiah"],
                            BungaNominal = r["BungaNominal"].Equals(System.DBNull.Value) ? 0 : (decimal)r["BungaNominal"],
                            BungaPersen = (string)r["BungaPersen"],
                            Gross = (decimal)r["Gross"],
                            NilaiDPP = (decimal)r["NilaiDPP"],
                            NilaiKlaim = (decimal)r["NilaiKlaim"],
                            NilaiPPN = (decimal)r["NilaiPPN"],
                            NilaiKontrak = (decimal)r["NilaiKontrak"],
                            NilaiRealisasiKPR = (decimal)r["NilaiRealisasiKPR"],
                            outbalance = (decimal)r["outBalance"],
                            PersenLunas = (decimal)r["persenLunas"],
                            TglKontrak = (DateTime)r["TglKontrak"],
                            Status = r["Status"].ToString(),
                            FlagProsesbatal = (int)r["FlagProsesBatal"],
                            NoST = r["NoST"].ToString(),
                            NoVA = r["NoVA"].ToString(),
                            TglBAST = r.Field<DateTime?>("TglST"),
                            TglPPJB = r.Field<DateTime?>("TglPPJB"),
                            TglBatal = r.Field<DateTime?>("TglBatal"),
                            StatusPPJB = r["PPJB"].ToString(),
                            StatusAJB = r["PPJB"].ToString(),
                            StatusST = r["ST"].ToString(),
                            NoPPJB = r.Field<string>("NoPPJB"),
                            KeteranganPPJB = r.Field<string>("KetPPJB"),
                            KeteranganBAST = r.Field<string>("KetBAST"),
                            KeteranganAJB = r.Field<string>("KetAJB"),
                        }).ToList();
            }
        }

        public static UnitJenis UnitJenis(object JenisID)
        {
            UnitJenis x = new UnitJenis();
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from Ref_Jenis where Jenis='" + JenisID + "'");
            if (rs.Rows.Count > 0)
            {
                var d = rs.Rows[0];
                x.JenisID = JenisID.ToString();
                x.Nama = d["Nama"].ToString();
            }
            return x;
        }
        public static List<UnitJenis> UnitJeniss
        {
            get
            {
                return (from DataRow r in Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from Ref_Jenis ").Rows
                        select new UnitJenis {
                            JenisID = r["Jenis"].ToString(),
                            Nama = r["Nama"].ToString()
                        }).ToList();
            }
        }

        public static UnitLokasi UnitLokasi(object LokasiID)
        {            
            return UnitLokasis.SingleOrDefault(p=>p.LokasiID == LokasiID.ToString());
        }
        public static List<UnitLokasi> UnitLokasis
        {
            get
            {
                return (from DataRow r in Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from Ref_Lokasi").Rows
                        select new UnitLokasi
                        {
                            LokasiID = r["Lokasi"].ToString(),
                            Nama = r["Nama"].ToString()
                        }).ToList();
            }
        }

        public static Unit Unit(object NoStock)
        {
            DataTable rsunit = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_unit where NoStock='" + NoStock + "'");
            if (rsunit.Rows.Count > 0)
            {
                Unit unit = new Unit();
                var runit = rsunit.Rows[0];
                unit.NoStock = NoStock.ToString();
                unit.NoUnit = runit["NoUnit"].ToString();
                unit.Status = runit["Status"].ToString();
                unit.PriceList = (decimal)runit["PriceList"];
                unit.Luas = (decimal)runit["Luas"];
                unit.LuasSemiGross = (decimal)runit["LuasSG"];
                unit.Jenis = runit["Jenis"].ToString();
                unit.Lokasi = runit["Lokasi"].ToString();
                unit.Panjang = (decimal)runit["Panjang"];
                unit.Lebar = (decimal)runit["Lebar"];
                unit.NamaJenis = Db.SingleString("use " + Mi.DbPrefix + "marketingjual;select Nama from ref_jenis where Jenis='" + runit["Jenis"] + "'");

                return unit;
            }
            return null;
        }
        public static List<Unit> Units
        {
            get
            {
                List<Unit> x = new List<Unit>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_unit");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Unit(r["NoStock"]));
                }
                return x;
            }
        }

        public static Customer Customer(object NoCustomer)
        {
            //Customer
            Customer cs = new Customer();
            #region Customer
            DataTable rscs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_customer where NoCustomer='" + NoCustomer + "'");
            if (rscs.Rows.Count > 0)
            {
                var rcs = rscs.Rows[0];

                cs.NoCustomer = NoCustomer.ToString();
                cs.Nama = rcs["Nama"].ToString();
                cs.Status = rcs["Status"].ToString();
                cs.Pekerjaan = rcs["pekerjaan"].ToString();
                cs.Kewarganegaraan = rcs["Kewarganegaraan"].ToString();
                cs.NPWP = rcs["NPWP"].ToString();
                cs.NoHP = rcs["NoHP"].ToString();
                cs.NoTelp = rcs["NoTelp"].ToString();
                cs.NoKantor = rcs["NoKantor"].ToString();
                cs.AlamatKTP1 = rcs["KTP1"].ToString();
                cs.AlamatKTP2 = rcs["KTP2"].ToString();
                cs.AlamatKTP3 = rcs["KTP3"].ToString();
                cs.AlamatKTP4 = rcs["KTP4"].ToString();
                cs.Alamat1 = rcs["Alamat1"].ToString();
                cs.Alamat2 = rcs["Alamat2"].ToString();
                cs.Alamat3 = rcs["Alamat3"].ToString();
                cs.Email = rcs["Email"].ToString();
                cs.NoKTP = rcs["NoKTP"].ToString();
                cs.NPWP = rcs["NPWP"].ToString();
            }
            #endregion

            return cs;
        }
        public static List<Customer> Customers
        {
            get
            {
                List<Customer> x = new List<Models.Customer>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_customer");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Customer(r["NoCustomer"]));
                }
                return x;
            }
        }

        public static Agent Agent(object NoAgent)
        {

            Agent ag = new Agent();
            #region Agent 
            DataTable rsag = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_agent where NoAgent='" + NoAgent + "'");
            if (rsag.Rows.Count > 0)
            {
                var rag = rsag.Rows[0];
                ag.NoAgent = NoAgent.ToString();
                ag.Nama = rag["NAMA"].ToString();
                ag.Status = rag["Status"].ToString();
                ag.NPWP = rag["NPWP"].ToString();
                ag.Tipe = rag["Tipe"].ToString();
            }

            #endregion

            return ag;
        }
        public static List<Agent> Agents
        {
            get
            {
                List<Agent> x = new List<Models.Agent>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from ms_agent");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Agent(r["NoAgent"]));
                }
                return x;
            }
        }

        public static SkemaCarabayar Skema(object SkemaID)
        {
            SkemaCarabayar x = new SkemaCarabayar();
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "Marketingjual;Select * from ref_skema where Nomor='" + SkemaID + "'");
            if (rs.Rows.Count > 0)
            {
                var r = rs.Rows[0];

                x.SkemaID = Convert.ToInt32(SkemaID);
                x.Status = (string)r["Status"];
                x.Nama = (string)r["Nama"];
                x.RThousand = (bool)r["RThousand"];
                x.Jenis = (string)r["Jenis"];
                x.Diskon = (string)r["Diskon"];
                x.DiskonKet = (string)r["DiskonKet"];
                x.Bunga = (string)r["Bunga"];
                x.BungaKet = (string)r["BungaKet"];
            }

            return x;
        }
        public static List<SkemaCarabayar> Skemas
        {
            get
            {
                List<SkemaCarabayar> x = new List<SkemaCarabayar>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "Marketingjual;Select * from ref_skema");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Skema(r["Nomor"]));
                }
                return x;
            }
        }

        public static Reservasi Reservasi(object NoReservasi)
        {
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from MS_Reservasi where NoReservasi='" + NoReservasi + "'");

            Reservasi a = new Reservasi();
            if (rs.Rows.Count > 0)
            {
                var d = rs.Rows[0];
                a.NoReservasi = NoReservasi.ToString().PadLeft(5, '0');
                a.Netto = (decimal)d["Netto"];
                a.NoQueue = (int)d["NoQueue"];
                a.Unit = Unit(d["NoStock"]);
                a.Agent = Agent(d["NoAgent"]);
                a.Customer = Customer(d["NoCustomer"]);
                a.NoUrut = (int)d["NoUrut"];
                a.TglReservasi = Convert.ToDateTime(d["Tgl"]);
                a.TglInput = Convert.ToDateTime(d["TglInput"]);
                a.TglEdit = Convert.ToDateTime(d["TglEdit"]);
                a.TglExpired = Convert.ToDateTime(d["TglExpire"]);
                a.Status = d["Status"].ToString();
                a.Skema = Skema(d["NoSkema"]);
                a.Acc = d["Acc"].ToString();
                a.PrintWL = (int)d["PrintWL"];
                a.PrintBF = (int)d["PrintBF"];
                a.ManualNoReservasi = d["ManualNoReservasi"].ToString();
            }
            return a;
        }
        public static List<Reservasi> Reservasis
        {
            get
            {
                List<Reservasi> x = new List<Models.Reservasi>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from MS_Reservasi");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Reservasi(r["NoReservasi"]));
                }
                return x;
            }
        }

        public static TTR TandaTerima(object NoTTR)
        {
            TTR a = new TTR();
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from MS_TTR where NoTTR='" + NoTTR + "'");
            if (rs.Rows.Count > 0)
            {
                var d = rs.Rows[0];
                a.NoTTR = NoTTR.ToString().PadLeft(5, '0');
                a.NoReservasi = d["NoReservasi"].ToString();
                a.CaraBayar = d["CaraBayar"].ToString();
                a.Keterangan = d["Keterangan"].ToString();
                a.Amount = (decimal)d["Total"];
                a.Status = d["Status"].ToString();
                a.PrintTTR = (int)d["PrintTTR"];
                a.TglInput = Convert.ToDateTime(d["TglInput"]);
                a.NoGiro = d["NoBG"].ToString();
                a.StatusGiro = d["StatusBG"].ToString();
                a.NilaiKembali = (decimal)d["NilaiKembali"];
                a.ManualTTR = d["ManualTTR"].ToString();
                a.Acc = d["Acc"].ToString();
            }
            return a;
        }
        public static List<TTR> TandaTerimas
        {
            get
            {
                List<TTR> x = new List<TTR>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select * from MS_TTR");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(TandaTerima(r["NoTTR"]));
                }
                return x;
            }
        }

        public static Kwitansi Kwitansi(object NoTTS)
        {
            Kwitansi x = new Kwitansi();
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "financear;SELECT * "
                + ",CASE CaraBayar"
                + "		WHEN 'TN' THEN 'TUNAI'"
                + "		WHEN 'KK' THEN 'KARTU KREDIT'"
                + "		WHEN 'KD' THEN 'KARTU DEBIT'"
                + "		WHEN 'TR' THEN 'TRANSFER BANK'"
                + "		WHEN 'BG' THEN 'CEK GIRO'"  
                + "		WHEN 'UJ' THEN 'UANG JAMINAN'"
                + "		WHEN 'DN' THEN 'DISKON'"
                + "		WHEN 'MB' THEN 'MERCHANT BANKING'"
                + "     WHEN 'PP' THEN 'PENGHAPUSAN PIUTANG'"
                + " END AS NamaCaraBayar"
                + " FROM MS_TTS WHERE NoTTS = '" + NoTTS + "'");
            if (rs.Rows.Count > 0)
            {
                var r = rs.Rows[0];
                x.NoKontrak = r["Ref"].ToString();
                x.NoTTS = NoTTS.ToString();
                x.NoBKM = r["NoBKM"].ToString();
                x.NoKwitansi = r["NoBKM2"].ToString();
                x.CaraBayar = (string)r["carabayar"];
                x.Keterangan = (string)r["Ket"];
                x.NilaiKwitansi = (decimal)r["Total"];
                x.Status = (string)r["Status"];
                x.TglTTS = (DateTime)r["TglTTS"];
                x.User = (string)r["UserID"];
                x.TglBKM = Cf.Day(r["TglBKM"]);
                x.TglInput = (DateTime)r["TglInput"];
                x.IPAddress = r["IP"].ToString();
                x.AccountID = r["Acc"].ToString();
                x.StatusBG = r["StatusBg"].ToString();
                x.TglBG = Cf.Day(r["TglBG"]);
                x.UdahJadiJurnal = (bool)r["Fobo"];
                x.Tipe = r["Tipe"].ToString();
                x.AdminBank = (decimal)r["AdminBank"];
                x.LebihBayar = (decimal)r["LebihBayar"];
                x.KurangBayar = (decimal)r["KurangBayar"];
                x.ManualTTS = r["manualTTS"].ToString();
                x.ManualBKM = r["ManualBKM"].ToString();
                x.NamaCaraBayar = r["NamaCaraBayar"].ToString();
                x.NoGiro = r["NoBG"].ToString();
            }
            return x;
        }
        public static List<Kwitansi> Kwitansis
        {
            get
            {
                List<Kwitansi> x = new List<Models.Kwitansi>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "marketingjual;Select NoTTS from MS_TTS ");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(Kwitansi(r["NoTTS"]));
                }
                return x;
            }
        }

        public static List<SkemaKomisiTermin> SkemaKomisiTermin(object SkemaID)
        {
            List<SkemaKomisiTermin> x = new List<SkemaKomisiTermin>();
            DataTable rs = Db.Rs("Select * from Ref_Skom_term where Nomor='" + SkemaID + "'");
            foreach (DataRow r in rs.Rows)
            {
                x.Add(new SkemaKomisiTermin
                {
                    SkemaID = Convert.ToInt32(SkemaID),
                    Baris = (int)r["Baris"],
                    Nama = r["nama"].ToString(),
                    BF = (bool)r["BF"],
                    NilaiBF = (decimal)r["NilaiBF"],
                    DP = (bool)r["DP"],
                    NilaiDP = (decimal)r["NilaiDP"],
                    ANG = (bool)r["ANG"],
                    NilaiANG = (decimal)r["NilaiANG"],
                    Lunas = (bool)r["Lunas"],
                    NilaiLunas = (decimal)r["NilaiLunas"],
                    Akad = (bool)r["Akad"],
                    PPJB = (bool)r["PPJB"],
                    Mode = (bool)r["Mode"]
                });
            }
            return x;
        }
        public static List<SkemaKomisiDetail> SkemaKomisiDetail(object SkemaID)
        {
            List<SkemaKomisiDetail> x = new List<SkemaKomisiDetail>();
            DataTable rs = Db.Rs("Select * from REF_SKOM_DETAIL where SkemaID='" + SkemaID + "'");
            foreach (DataRow r in rs.Rows)
            {
                x.Add(new SkemaKomisiDetail
                {
                    SkemaID = Convert.ToInt32(SkemaID),
                    Baris = Convert.ToInt32(r["baris"]),
                    Nama = r["Nama"].ToString(),
                    Nominal = (decimal)r["Nominal"],
                    TipeNominal = r["TipeNominal"].ToString(),
                    NilaiBF = (decimal)r["NilaiBF"],
                    TipeBF = r["TipeBF"].ToString(),
                    Target = (decimal)r["Target"],
                    TipeTarget = r["TipeTarget"].ToString(),
                    NilaiNUP = (decimal)r["NilaiNUP"],
                    TipeNUP = r["TipeNUP"].ToString(),


                });
            }
            return x;
        }
        public static SkemaKomisi SkemaKomisi(object SkemaID)
        {
            SkemaKomisi x = new SkemaKomisi();

            return x;
        }

        public static FakturPajak FakturPajak(string NoFakturPajak)
        {
            DataTable rs = Db.Rs("use " + Mi.DbPrefix + "Financear;select * from Ref_FP Where NoFPS='" + NoFakturPajak + "'");
            if (rs.Rows.Count > 0)
            {
                var d = rs.Rows[0];
                return new Models.FakturPajak
                {
                    NoFaktur = d["NoFPS"].ToString(),
                    Status = (bool)d["Status"]
                };
            }
            return new FakturPajak();
        }
        public static List<FakturPajak> FakturPajaks
        {
            get
            {
                List<FakturPajak> x = new List<Models.FakturPajak>();
                DataTable rs = Db.Rs("use " + Mi.DbPrefix + "Financear;select * from Ref_FP");
                foreach (DataRow r in rs.Rows)
                {
                    x.Add(new FakturPajak
                    {
                        NoFaktur = r["NoFPS"].ToString(),
                        Status = (bool)r["Status"]
                    });
                }

                return x;
            }
        }

        public static GantiUnit KontrakGantiUnit(object NoKontrak)
        {
            var d = new GantiUnit();
            var rs = Db.Rs("Select top 1 * from ms_kontrak_gantiunit where NoKontrak ='"+ NoKontrak +"' and Status=0 order by SN desc");
            if(rs.Rows.Count > 0)
            {
                var r = rs.Rows[0];
                d.NoKontrak = NoKontrak.ToString();
                d.SN = Convert.ToInt32(r["SN"]);
                d.NoStockLama = r["NoStockLama"].ToString();
                d.NoStockBaru = r["NoStockBaru"].ToString();
                d.Tgl = Convert.ToDateTime(r["Tgl"]);
                d.TglInput = Convert.ToDateTime(r["TglInput"]);
                if(!(r["TglApprove"] is DBNull))
                {
                    d.TglApprove = Convert.ToDateTime(r["TglApprove"]);
                }
                d.Approved = (bool)r["Approved"];
                d.BiayaAdmin = Convert.ToDecimal(r["BiayaAdmin"]);
                return d;
            }else
            {
                return null;
            }
        }
    }


    public class Reservasi
    {
        public string NoReservasi;
        public string ManualNoReservasi;
        public int NoUrut;
        public string Status;
        public DateTime TglReservasi;
        public DateTime TglInput;
        public DateTime TglExpired;
        public DateTime TglEdit;
        public decimal Netto;
        public int NoQueue;
        public int PrintBF;
        public int PrintWL;
        public int PrintPP;
        public Unit Unit;
        public Customer Customer;
        public Agent Agent;
        public SkemaCarabayar Skema;
        public TTR TandaTerima;
        public string Acc;
    }
    public class TTR
    {
        public string NoTTR;
        public string NoReservasi;
        public DateTime TglTTR;
        public DateTime TglInput;
        public string CaraBayar;
        public string Keterangan;
        public decimal Amount;
        public string Status;
        public int PrintTTR;
        public string NoGiro;
        public string StatusGiro;
        public decimal NilaiKembali;
        public string ManualTTR;
        public string Acc;

    }

    public class Kontrak
    {

        public string NoKontrak;
        public string NoAgent;
        public string NoCustomer;
        public string Status;
        public DateTime TglKontrak;
        public DateTime? TglBatal;
        public DateTime? TglPPJB;
        public DateTime? TglBAST;
        public decimal Gross;
        public decimal DiskonRupiah;
        public decimal DiskonTambahan;
        public string DiskonPersen;
        public decimal BungaNominal;
        public string BungaPersen;
        public decimal NilaiKontrak;
        public decimal NilaiDPP;
        public decimal NilaiPPN;
        public string CaraBayar;
        public decimal outbalance;
        public decimal PersenLunas;
        public decimal NilaiRealisasiKPR;
        public decimal NilaiKlaim;
        public int FlagProsesbatal;
        public string NoST;
        public string NoVA;
        public string StatusPPJB;
        public string StatusST;
        public string StatusAJB;
        public string NoPPJB;
        public string KeteranganPPJB;
        public string KeteranganAJB;
        public string KeteranganBAST;
        public Unit Unit;
        public Agent Agent;
        public Customer Customer;
        public SkemaCarabayar SkemaCaraBayar;
    }
    public class Unit
    {
        public string NoStock;
        public string Status;
        public string Jenis;
        public string Lokasi;
        public string NoUnit;
        public decimal Luas;
        public decimal LuasSemiGross;
        public decimal LustNett;
        public decimal Panjang;
        public decimal Lebar;
        public decimal Tinggi;
        public decimal PriceList;
        public string NamaJenis;

    }
    public class UnitJenis
    {
        public string JenisID;
        public string Nama;

    }
    public class UnitLokasi
    {
        public string LokasiID;
        public string Nama;
    }
    public class Customer
    {
        public string NoCustomer;
        public string Status;
        public string Nama;
        public string NoTelp;
        public string NoHP;
        public string NoKantor;
        public string Email;
        public string NoKTP;
        public string AlamatKTP1;
        public string AlamatKTP2;
        public string AlamatKTP3;
        public string AlamatKTP4;
        public string Alamat1;
        public string Alamat2;
        public string Alamat3;
        public string NPWP;
        public string Kewarganegaraan;
        public string Pekerjaan;
    }
    public class Agent
    {
        public string NoAgent;
        public string Status;
        public string Nama;
        public string NPWP;
        public string Tipe;
    }
    public class Pelunasan
    {


        public string NoKontrak;
        public int SN;
        public int NoTagihan;
        public string CaraBayar;
        public string Keterangan;
        public DateTime TglPelunasan;
        public decimal NilaiPelunasan;
        public bool SudahCair;
        public string NoTTS;
        public string NoBKM;
        public string NoMEMO;
        public List<Kwitansi> ListKwitansi;
        public decimal Alokasi;
    }
    public class Tagihan
    {
        public string NoKontrak;
        public int NoTagihan;
        public string NamaTagihan;
        public DateTime TglJT;
        public decimal NilaiTagihan;
        public string Tipe;
        public bool FlagPJT;
        public decimal NilaiDenda;
        public decimal NilaiDendaReal;
        public bool isKPR;
        public bool Akad;
        public bool PutihDenda;
        public decimal NilaiPutihDenda;
        public List<Pelunasan> ListPelunasan;
        public decimal SisaTagihan;
        public decimal LebihBayar;
    }
    public class Kwitansi
    {
        public string NoKontrak;
        public string NoTTS;
        public string NoBKM;
        public string NoKwitansi;
        public DateTime TglTTS;
        public string TglBKM;
        public decimal NilaiKwitansi;
        public string User;
        public string CaraBayar;
        public string NamaCaraBayar;
        public string Keterangan;
        public string Status;
        public DateTime TglInput;
        public string IPAddress;
        public string Tipe;
        public string StatusBG;
        public string TglBG;
        public string JurnalID;
        public string AccountID;
        public decimal KurangBayar;
        public decimal LebihBayar;
        public bool UdahJadiJurnal;
        public decimal AdminBank;
        public string ManualTTS;
        public string ManualBKM;
        public string NoGiro;
    }
    public class SkemaCarabayar
    {
        public int SkemaID;
        public string Nama;
        public string Jenis;
        public string Status;
        public bool RThousand;
        public string Diskon;
        public string DiskonKet;
        public string Bunga;
        public string BungaKet;
    }


    public class SkemaKomisi
    {
        public int SkemaID;
        public string Nama;
        public string Status;
        public string Tipe;
        public DateTime PeriodeStart;
        public DateTime PeriodeEnd;
        public List<SkemaKomisiDetail> SkemaDetail;
    }
    public class SkemaKomisiDetail
    {
        public int SkemaID;
        public int Baris;
        public string Tipe;
        public string Nama;
        public decimal Nominal;
        public string TipeNominal;
        public decimal Target;
        public string TipeTarget;
        public decimal NilaiBF;
        public string TipeBF;
        public decimal NilaiNUP;
        public string TipeNUP;
        public List<SkemaKomisiTermin> Termin;
    }
    public class SkemaKomisiTermin
    {
        public int SkemaID;
        public int Baris;
        public string Nama;
        public bool Lunas;
        public decimal NilaiLunas;
        public bool BF;
        public decimal NilaiBF;
        public bool DP;
        public decimal NilaiDP;
        public bool ANG;
        public decimal NilaiANG;
        public bool PPJB;
        public bool Akad;
        public bool Mode;
    }

    public class FakturPajak
    {
        public string NoFaktur;
        public bool Status;
    }
    public class GantiUnit {
        public string NoKontrak;
        public int SN;
        public bool Status;
        public string NoStockLama;
        public string NoStockBaru;
        public DateTime Tgl;
        public DateTime TglInput;
        public DateTime TglApprove;
        public bool Approved;
        public string ApprovedBy;
        public string Keterangan;
        public decimal BiayaAdmin;
    }
}

//Ano hi miwatashita nagisa wo ima mo omoidasunda
//Suna no ue ni kizanda kotoba  kimi no ushirosugata

//Yorikaesu nami ga  ashimoto wo yogiri nanika wo sarau
//Yuunagi no naka higure dake ga toorisugite yuku

//PA tto hikatte saita  hanabi wo miteita
//Kitto mada owaranai natsu ga
//Aimai na kokoro wo  tokashite tsunaida
//Kono yoru ga tsuzuite hoshikatta 

//"Ato nando kimi to onaji hanabi wo mirareru kana" tte
//Warau kao ni nani ga dekiru darou ka
//Kizutsuku koto yorokobu koto kurikaesu namida to joudou
//Shousou  saishuu ressha no oto

//Nando demo  kotoba ni shite kimi wo yobu yo
//Namima wo erabi  mou ichido.. do.. do.. do..
//Mou nidoto kanashimazu ni sumu you ni

//Ha tto iki wo nomeba  kiechaisou na hikari ga
//Kitto mada  mune ni sundeita
//Te wo nobaseba fureta attakai mirai wa
//Hisoka ni futari wo miteita

//PA tto hanabi ga
//Yoru ni saita
//Yoru ni saita
//Shizuka ni kieta
//Hanasanai de
//Mou sukoshi dake
//Mou sukoshi dake
//Kono mama de

//Ano hi miwatashita nagisa wo  ima mo omoidasunda
//Suna no ue ni kizanda kotoba kimi no ushirosugata

//PA tto hikatte saita  hanabi wo miteita
//Kitto mada owaranai natsu ga
//Aimai na kokoro wo  tokashite tsunaida
//Kono yoru ga tsuzuite hoshikatta