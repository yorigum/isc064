<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HtmlEditorHelp.aspx.cs" Inherits="HtmlEditorHelp" %>

<%@ Register TagPrefix="uc1" TagName="Head" Src="Head.ascx" %>
<!DOCTYPE html>
<html>
<head>
    <title>Html Editor Help</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link href="/Media/Style.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .tb td {
            vertical-align: top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:Head ID="Head1" runat="server"></uc1:Head>
        <h1 class="title title-line">Html Editor - Help</h1>

        <table class="tb blue-skin">
            <tr>
                <th></th>
                <th>Keterangan
                </th>
                <th>Hasil
                </th>
                <th>Kondisi
                </th>
            </tr>
            <tr>
                <td>
                    <h2>Parameter</h2>
                </td>
                <td>Parameter dapat digunakan menggunakan simbol @@, maka sistem akan menggantikan teks tersebut dengan parameter yang tersedia.<br />
                    Contoh : @@NilaiKontrak<br />
                </td>
                <td>110,000,000
                </td>
                <td>Parameter yang akan digunakan, hanya yang sudah disediakan oleh sistem untuk halaman tersebut.
                </td>
            </tr>
            <tr>
                <td>
                    <h2>Rumus</h2>
                </td>
                <td>Rumus dapat digunakan menggunakan simbol {{ }}.<br />
                    Contoh : "Nilai PPN adalah : {{ @@NilaiKontrak - @@NilaiDPP }}"<br />
                    Maka sistem akan mengubahnya menjadi pengurangan antara nilai kontrak dan nilai dpp
                </td>
                <td>Nilai PPN adalah : 10,000,000
                </td>
                <td>
                    <ol>
                        <li>Penggunaan rumus harus menyisipkan spasi setelah simbol kurawal buka dan sebelum kurawal tutup.
                        </li>
                        <li>Apabila menggunakan parameter, maka hanya bisa menggunakan yang memiliki tipe Data Nilai.
                        </li>
                    </ol>
                    <br />
                    Penggunaan operator matematika yang bisa digunakan adalah : 
                    <ol>
                        <li>* Untuk perkalian</li>
                        <li>/ Untuk pembagian</li>
                        <li>+ Untuk penjumlahan</li>
                        <li>- Untuk pengurangan</li>
                        <li>^ Untuk pangkat</li>
                    </ol>
                </td>
            </tr>
            <tr>
                <td>
                    <h2>Tabel Perulangan
                    </h2>
                </td>
                <td>Tabel perulangan dapat digunakan dengan menggunakan simbol
                    &lt;!-- $StartLooping --&gt; lalu ditutup dengan simbol
                    &lt;!-- $EndLooping --&gt;<br />
                    Contoh dalam bentuk source :
                    <pre> 
    &lt;table border=&quot;1&quot; cellpadding=&quot;1&quot; cellspacing=&quot;1&quot; style=&quot;width:500px&quot;&gt; &lt;tbody&gt; 
        &lt;tr&gt; 
            &lt;td&gt;Nama Tagihan&lt;/td&gt; 
            &lt;td&gt;Tanggal Jatuh Tempo&lt;/td&gt; 
            &lt;td&gt;Nilai Tagihan&lt;/td&gt; 
        &lt;/tr&gt; 
    <span style="color:red">
            &lt;!-- $StartLooping --&gt; 
        </span>
        &lt;tr&gt; 
            &lt;td&gt;@@NamaTagihan&lt;/td&gt; 
            &lt;td&gt;@@TanggalJT&lt;/td&gt; 
            &lt;td&gt;@@NilaiTagihan&lt;/td&gt; 
        &lt;/tr&gt; 
    <span style="color:red">
            &lt;!-- $EndLooping --&gt; 
        </span>
        &lt;/tbody&gt; 
    &lt;/table&gt; </pre>
                </td>
                <td>
                    <table>
                        <tbody border="1" cellpadding="1" cellspacing="1" style="width: 500px">
                            <tr>
                                <td>Nama Tagihan</td>
                                <td>Tanggal Jatuh Tempo</td>
                                <td>Nilai Tagihan</td>
                            </tr>
                            <!-- $StartLooping -->
                            <tr>
                                <td>BOOKING FEE</td>
                                <td>21 Oct 2017</td>
                                <td>10,000,000</td>
                            </tr>
                            <!--  -->
                            <tr>
                                <td>DOWN PAYMENT # 1</td>
                                <td>25 Nov 2017</td>
                                <td>4,000,000</td>
                            </tr>
                            <!--  -->
                            <tr>
                                <td>DOWN PAYMENT  # 2</td>
                                <td>25 Dec 2017</td>
                                <td>4,000,000</td>
                            </tr>
                        </tbody>
                    </table>
                </td>
                <td>Tabel yang bisa diterapkan hanya yang datanya telah di sediakan oleh sistem.
                </td>
            </tr>
            <tr>
                <td>
                    <h2>Template
                    </h2>
                </td>
                <td>Html editor dapat menyisipkan halaman dari template yang lainnya.<br />
                    Penggunaannya harus menggunakan simbol ### dan diapit dengan &lt;!-- $StartLoad --&gt;&lt;!-- $EndLoad --&gt;<br />
                    Contoh : Di Template SuratPesanan kita sisipkan simbol <span style="color: red">
                        <pre>
    &lt;!-- $StartLoad --&gt; 
        ###JadwalTagihan
    &lt;!-- $EndLoad --&gt; 
</pre>
                    </span>
                </td>
                <td>Maka di halaman SuratPesanan akan muncul jadwal tagihan seperti yang sudah kita terapkan pada HTML Editor.
                </td>
                <td>
                    <ol>
                        <li>Nama Template harus sesuai dengan yang tersedia di dalam sistem. Diperkenankan untuk berbeda modul.
                        </li>
                        <li>Antara template utama dan template yang disisipkan, harus memiliki Identitas Data yang sama. Contoh : Nomor Kontrak.
                        </li>
                    </ol>
                </td>
            </tr>
            <tr>
                <td>
                    <h2>Kondisi</h2>
                </td>
                <td>Html editor dapat menggunakan kondisi tertentu.<br />
                    Penggunaannya dengan menggunakan simbol &lt;!-- $StartIF --&gt;@@IF(kondisi yang diinginkan) {{{ hasilkondisi }}} lalu ditutup dengan simbol &lt;!-- $EndtIF --&gt;<br />
                    Contoh : Di Template DetilTTS kita sisipkan kondisi untuk memberi kondisi jika ada Admin Bank dll.
                    <span style="color:red">
                    <pre>
                        &lt;!-- $StartIF 
                            @@IF(@@AdminBank > 0)
                                {{{ --&gt; 
                        &lt;tr&gt;
                            &lt;td&gt;Admin Bank &lt;td&gt;
                            &lt;td&gt;@@AdminBank &lt;td&gt
                        &lt;tr&gt; &lt;!-- }}}
                         $EndIF --&gt; 
                    </pre>
                    </span>
                </td>
                <td>Maka di kotak pilihan silang pada Template SuratPesanan akan menghasilkan tanda X ketika kondisi yang diberikan bernilai True.</td>
                <td>Parameter yang akan digunakan untuk kondisi, hanya yang sudah disediakan oleh sistem untuk halaman tersebut.</td>
            </tr>
        </table>
    </form>
</body>
</html>
