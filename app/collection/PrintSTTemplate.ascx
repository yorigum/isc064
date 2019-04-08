<%@ Control Language="c#" Inherits="ISC064.COLLECTION.PrintSTTemplate" CodeFile="PrintSTTemplate.ascx.cs" %>
<div id="content" runat="server"></div>

<style>
    .allBorder {
        border-top: 1px solid black;
        border-right: 1px solid black;
        border-bottom: 1px solid black;
        border-left: 1px solid black;
        margin: 0px;
        padding: 0px;
    }
</style>

<div style="width: 100%;">
    <asp:Table ID="TabelTop" runat="server" Width="100%">
        <asp:TableRow>
            <asp:TableCell HorizontalAlign="Left">
                No. :
                <asp:Label ID="lblno" runat="server"></asp:Label>
            </asp:TableCell>
            <asp:TableCell HorizontalAlign="Right">
                Cikarang,
                <asp:Label ID="lbltgl" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table ID="TableHead" runat="server">
        <asp:TableRow>
            <asp:TableCell>Kepada Yth.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblnama" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                <asp:Label ID="lblalamat" runat="server"></asp:Label>
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Perihal : <b>Surat&nbsp;<asp:Label ID="lblperingatan" runat="server"></asp:Label></b></asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>Dengan hormat,</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>
                Sehubungan atas pembelian kavling di SAVASA, <b>CLUSTER 
                    <asp:Label ID="lblnounitalmt" runat="server"></asp:Label>.</b>
                Kami beritahukan kewajiban pembayaran Saudara yang telah jatuh tempo, dengan perincian sebagai berikut :
            </asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table ID="TableTag" runat="server" Width="100%" CellSpacing="0">
        <asp:TableRow>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Keterangan</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Angsuran</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Jatuh Tempo</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Realisasi Pembayaran</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Tanggal Bayar</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Denda Hari</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Denda</asp:TableCell>
            <asp:TableCell CssClass="allBorder" HorizontalAlign="Center">Jumlah</asp:TableCell>
        </asp:TableRow>
    </asp:Table>

    <asp:Table ID="TableBot" runat="server">
        <asp:TableRow>
            <asp:TableCell><i>(Nilai denda akan diperhitungkan kembali sesuai dengan tanggal realisasi pembayaran angsuran)</i></asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div>
    <asp:Table ID="TableBot1" runat="server">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Kami mengharapkan agar Saudara dapat menyelesaikan segera 
                pembayaran seperti tertera diatas selambat-lambatnya 7 (tujuh) 
                hari sejak tanggal surat ini kepada kasir kami :
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>1.</asp:TableCell>
            <asp:TableCell>MARKETING GALLERY SAVASA</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>Jl.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>(Dapat menggunakan fasilitas debet, pembayaran dengan kartu kredit akan dikenakan biaya tambahan).</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>2.</asp:TableCell>
            <asp:TableCell>Atau dengan cara transfer ke rekening : PT. PANAHOME DELTAMAS INDONESIA</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>BANK SINARMAS Cab. KCU THAMRIN - JAKARTA Rek.No : 0009588768 Bank Code : 153</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Apabila Saudara memerlukan penjelasan lebih lanjut, dapat menghubungi bagian Collection dengan Sdri.(nama menyusul) melalui No.Telpn. (021)…(nomor menyusul)
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">Mohon abaikan surat ini apabila Saudara telah melakukan pembayaran.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">Atas perhatiannya kami ucapkan terima kasih.</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>

<div>
    <asp:Table ID="TableBot2" runat="server">
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Sehubungan Saudara telah menunggak pembayaran angsuran selama 4 (empat) bulan berturut-turut, 
                maka sesuai dengan PPJB (Pengikatan Perjanjian Jual Beli) pasal ……………………………… 
                yang telah ditanda tangani Bersama pada tanggal …………………<asp:Label ID="tglttd" runat="server"></asp:Label>
                Maka dengan ini kami sampaikan bahwa jika dalam kurun waktu 7 (tujuh) hari sejak surat ini 
                diterbitkan dan masih belum ada pembayaran apapun dari Saudara, transaksi ini kami nyatakan 
                BATAL demi hukum dan seluruh pembayaran yang telah kami terima dinyatakan HANGUS.
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Pembayaran dapat dilakukan :
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>1.</asp:TableCell>
            <asp:TableCell>MARKETING GALLERY SAVASA</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>Jl.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>(Dapat menggunakan fasilitas debet, pembayaran dengan kartu kredit akan dikenakan biaya tambahan biaya).</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>2.</asp:TableCell>
            <asp:TableCell>Atau dengan cara transfer ke rekening : PT. PANAHOME DELTAMAS INDONESIA</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell></asp:TableCell>
            <asp:TableCell>BANK SINARMAS Cab. KCU THAMRIN - JAKARTA Rek.No : 0009588768 Bank Code : 153</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Demikian informasi ini kami sampaikan, agar mendapat perhatian segera.
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">
                Apabila Saudara memerlukan penjelasan lebih lanjut, dapat menghubungi bagian Collection dengan Sdri.(nama menyusul) melalui No.Telpn. (021)…(nomor menyusul)
            </asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">Mohon abaikan surat ini apabila Saudara telah melakukan pembayaran.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">Atas perhatiannya kami ucapkan terima kasih.</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell>&nbsp;</asp:TableCell>
        </asp:TableRow>
        <asp:TableRow>
            <asp:TableCell ColumnSpan="3">Hormat Kami</asp:TableCell>
        </asp:TableRow>
    </asp:Table>
</div>
