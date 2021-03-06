﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PemanggilanTampil.aspx.cs" Inherits="ISC064.SECURITY.PemanggilanTampil" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>DISPLAY PEMANGGILAN</title>
    <link href="../Media/Style.css" type="text/css" rel="stylesheet" />
    <link href="css/Custom.css" type="text/css" rel="stylesheet" />
    <script src="/Js/JQuery.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div class="row" id="headwrap">
                <div class="col s6">
                    <div class="col s6" style="margin-left: 10px; padding-left: 10px !important; border-left: 10px #0cb2dc solid;">
                        <div class="col s3" style="float: left; text-align: left;">
                            <img src="Image/panasonic.png" style="width: 300px; height: 70px;" />
                        </div>
                        <div class="col s3" style="float: right; text-align: right; margin-left: 100px;">
                            <img src="Image/sinarmas.png" style="width: 300px; height: 70px;" />
                        </div>
                    </div>
                    <div class="col s3" style="float: right; text-align: right; display: none;">
                        <span class="tanggal" id="tglspan" runat="server"></span>
                        <br />
                        <span class="jam">
                            <div class="clock">
                                <div class="hours">
                                    <div class="first">
                                        <div class="number">0</div>
                                    </div>
                                    <div class="second">
                                        <div class="number">0</div>
                                    </div>
                                </div>
                                <div class="tick">:</div>
                                <div class="minutes">
                                    <div class="first">
                                        <div class="number">0</div>
                                    </div>
                                    <div class="second">
                                        <div class="number">0</div>
                                    </div>
                                </div>
                                <div class="tick">:</div>
                                <div class="seconds">
                                    <div class="first">
                                        <div class="number">0</div>
                                    </div>
                                    <div class="second infinite">
                                        <div class="number">0</div>
                                    </div>
                                </div>
                            </div>
                        </span>
                    </div>
                </div>
                <div class="col s6">
                    <div class="col s3" style="float: right; text-align: right; margin-right: 100px;">
                        <img src="Image/logosavasa.png" style="width: 200px; height: 70px;" />
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col s12" style="text-align: center; padding: 50px 500px; margin: 10px;">
                    <div>
                        <h1>NUP TERPANGGIL</h1>
                    </div>
                    <br style="clear: both;" />
                    <p>
                        <span id="nup">-</span>
                    </p>
                    <p class="big">
                        di LOKET <span id="counter">-</span>
                    </p>
                    <div class="row" style="margin-top: 50px;">
                        <div class="col s3" style="border-left: 20px solid #CCC;">COUNTER OFF</div>
                        <div class="col s3" style="border-left: 20px solid #000;">DIPANGGIL</div>
                        <div class="col s3" style="border-left: 20px solid #0850a1;">MEMILIH UNIT</div>
                        <div class="col s3" style="border-left: 20px solid #ec343b;">TIDAK DATANG</div>
                    </div>
                </div>
            </div>
            <div class="row" id="counterlist">
            </div>
            <div style="display: none;">
                <audio controls="controls" src="/Media/audio/satu.wav" id="satu"></audio>
                <audio controls="controls" src="/Media/audio/dua.wav" id="dua"></audio>
                <audio controls="controls" src="/Media/audio/tiga.wav" id="tiga"></audio>
                <audio controls="controls" src="/Media/audio/empat.wav" id="empat"></audio>
                <audio controls="controls" src="/Media/audio/lima.wav" id="lima"></audio>
                <audio controls="controls" src="/Media/audio/enam.wav" id="enam"></audio>
                <audio controls="controls" src="/Media/audio/tujuh.wav" id="tujuh"></audio>
                <audio controls="controls" src="/Media/audio/delapan.wav" id="delapan"></audio>
                <audio controls="controls" src="/Media/audio/sembilan.wav" id="sembilan"></audio>
                <audio controls="controls" src="/Media/audio/sepuluh.wav" id="sepuluh"></audio>
                <audio controls="controls" src="/Media/audio/sebelas.wav" id="sebelas"></audio>
                <audio controls="controls" src="/Media/audio/belas.wav" id="belas"></audio>
                <audio controls="controls" src="/Media/audio/puluh.wav" id="puluh"></audio>
                <audio controls="controls" src="/Media/audio/seratus.wav" id="seratus"></audio>
                <audio controls="controls" src="/Media/audio/ratus.wav" id="ratus"></audio>
                <audio controls="controls" src="/Media/audio/seribu.wav" id="seribu"></audio>
                <audio controls="controls" src="/Media/audio/ribu.wav" id="ribu"></audio>
                <audio controls="controls" src="/Media/audio/loket.wav" id="loket"></audio>
                <audio controls="controls" src="/Media/audio/blank.wav" id="blank"></audio>
                <audio controls="controls" src="/Media/audio/nomor-urut.wav" id="nomor-urut"></audio>
            </div>
        </div>
        <script type="text/javascript">

            var adaplaylist = false;
            var PlayList = undefined;

            $(document).ready(function () {
                setInterval(function () {

                    $.getJSON("PemanggilanTampil.aspx?act=listcounter", function (data) {
                        var html = "";
                        $.each(data, function (i, item) {
                            html += '<div class="col s2setengah card">'
                            html += '<div class="col s8" style="height:5px; background:#0cb2dc;"></div>'
                            html += '<div class="col s12 panel-title">PEMILIHAN UNIT</div>'
                            html += '<div class="panel-content ' + item['Warna'] + ' ">' + item['Nomor'] + '</div>'
                            html += '<div class="bawah">'
                            html += '<div class="col s8 bottom-panel">LOKET</div>'
                            html += '<div class="col triangle"></div>'
                            html += '<div class="col s11 cname">' + item['Nama'] + '</div>'
                            html += '</div></div>';
                        });
                        $('#counterlist').html(html);
                    });

                    if (!adaplaylist) {
                        // biar gak double load
                        // ambil data dari db

                        console.log('loading');
                        $.getJSON("PemanggilanTampil.aspx?act=calllist", function (data) {
                            console.log(data);
                            PlayList = data;
                            adaplaylist = true;

                            if (PlayList[0] != undefined)
                                Play(PlayList, 0, PlayList[0]['Text'].split(' ')[0], 0);

                            // jika gak ada data nya lanjut request
                            else adaplaylist = false;
                        });
                    }
                }, 1000);
            });

            function Play(tracks, queue, word, index) {
                var audio = document.getElementById(word);
                if (audio != undefined) {
                    $('#nup').html(tracks[queue]['NUPID']);
                    $('#counter').html(tracks[queue]['Counter']);
                    try { audio.play(); } catch (e) { adaplaylist = false; }
                    audio.onended = function () {
                        // play data per kata
                        if (index < tracks[queue]['Text'].split(' ').length - 1) {
                            index++;
                            Play(
                                tracks,
                                queue,
                                PlayList[queue]['Text'].split(' ')[index],
                                index
                            );

                        } else {
                            // update status di db jadi called
                            $.getJSON("PemanggilanTampil.aspx?act=updatestatus&id=" + PlayList[queue]['ID'], function (data) { });
                            if (PlayList.length < queue)
                                Play(
                                    tracks,
                                    ++queue,
                                    PlayList[queue]['Text'].split(' ')[0],
                                    0
                                );
                            // antrian abis load dari db lagi
                            else adaplaylist = false;

                        }
                    }
                }

            }

        </script>
        <script type="text/javascript">
            var hoursContainer = document.querySelector('.hours')
            var minutesContainer = document.querySelector('.minutes')
            var secondsContainer = document.querySelector('.seconds')
            var tickElements = Array.from(document.querySelectorAll('.tick'))

            var last = new Date(0)
            last.setUTCHours(-1)

            var tickState = true

            function updateTime() {
                var now = new Date

                var lastHours = last.getHours().toString()
                var nowHours = now.getHours().toString()
                if (lastHours !== nowHours) {
                    updateContainer(hoursContainer, nowHours)
                }

                var lastMinutes = last.getMinutes().toString()
                var nowMinutes = now.getMinutes().toString()
                if (lastMinutes !== nowMinutes) {
                    updateContainer(minutesContainer, nowMinutes)
                }

                var lastSeconds = last.getSeconds().toString()
                var nowSeconds = now.getSeconds().toString()
                if (lastSeconds !== nowSeconds) {
                    //tick()
                    updateContainer(secondsContainer, nowSeconds)
                }

                last = now
            }

            function tick() {
                tickElements.forEach(t => t.classList.toggle('tick-hidden'))
            }

            function updateContainer(container, newTime) {
                var time = newTime.split('')

                if (time.length === 1) {
                    time.unshift('0')
                }


                var first = container.firstElementChild
                if (first.lastElementChild.textContent !== time[0]) {
                    updateNumber(first, time[0])
                }

                var last = container.lastElementChild
                if (last.lastElementChild.textContent !== time[1]) {
                    updateNumber(last, time[1])
                }
            }

            function updateNumber(element, number) {
                //element.lastElementChild.textContent = number
                var second = element.lastElementChild.cloneNode(true)
                second.textContent = number

                element.appendChild(second)
                element.classList.add('move')

                setTimeout(function () {
                    element.classList.remove('move')
                }, 990)
                setTimeout(function () {
                    element.removeChild(element.firstElementChild)
                }, 990)
            }

            setInterval(updateTime, 100)
        </script>
    </form>
</body>
</html>
