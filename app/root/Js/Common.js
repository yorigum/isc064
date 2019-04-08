/* Close Browser */
function browserClose() {
    if (window.event.clientY < 0 && window.event.clientY < -80) {
        openPopUp('/SignOut.aspx?close=1', '300', '100');
    }
}

function openLaporan(myFile) {
    myWidth = 950;
    myHeight = 550;
    myWindowName = "_blank";
    LeftPosition = (screen.width) ? (screen.width - myWidth) / 2 : 100;
    TopPosition = 10;
    lockWindow = window.open(myFile, myWindowName, "height=" + myHeight + ",width=" + myWidth + ",channelmode=0,dependent=0,directories=0,fullscreen=0,top=" + TopPosition + ",left=" + LeftPosition + ",menubar=1,resizable=1,scrollbars=1,status=0,toolbar=1")
}

function openPrint(myFile) {
    myWidth = 780;
    myHeight = 550;
    myWindowName = "_blank";
    LeftPosition = (screen.width) ? (screen.width - myWidth) / 2 : 100;
    TopPosition = 10;
    lockWindow = window.open(myFile, myWindowName, "height=" + myHeight + ",width=" + myWidth + ",channelmode=0,dependent=0,directories=0,fullscreen=0,top=" + TopPosition + ",left=" + LeftPosition + ",menubar=1,resizable=1,scrollbars=1,status=0,toolbar=1")
}

function openCalendar(ctrl) {
    foo = document.getElementById(ctrl);
    openModal("/Calendar.aspx?ctrl=" + ctrl + "&date=" + foo.value, "350", "350");
}
function getCalendar(ctrl, date) {
    document.getElementById(ctrl).value = date.replace(" 12:00:00 AM", "");
}

function openPopUp(myFile, myWidth, myHeight) {
    //PopupCenter(myFile, "", myWidth, myHeight);
    if (myWidth == "") myWidth = 500;
    if (myHeight == "") myHeight = 600;
    myWindowName = "_blank";
    LeftPosition = (screen.width) ? (screen.width - myWidth) / 2 : 100;
    TopPosition = 10;
    lockWindow = window.open(myFile, myWindowName, "height=" + myHeight + ",width=" + myWidth + ",channelmode=0,dependent=0,directories=0,fullscreen=0,top=" + TopPosition + ",left=" + LeftPosition + ",menubar=1,resizable=1,scrollbars=1,status=0,toolbar=0")
}

function openModal(myFile, myWidth, myHeight) {
    if (myWidth == "") myWidth = 500;
    if (myHeight == "") myHeight = 600;
    myWindowName = "_blank";
    LeftPosition = (screen.width) ? (screen.width - myWidth) / 2 : 100;
    TopPosition = 10;
    lockWindow = window.open(myFile, myWindowName, "height=" + myHeight + ",width=" + myWidth + ",channelmode=0,dependent=0,directories=0,fullscreen=0,top=" + TopPosition + ",left=" + LeftPosition + ",menubar=1,resizable=1,scrollbars=1,status=0,toolbar=0")
}

function PopupCenter(url, title, w, h) {
    // Fixes dual-screen position                         Most browsers      Firefox
    var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
    var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

    var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
    var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

    var left = ((width / 2) - (w / 2)) + dualScreenLeft;
    var top = ((height / 2) - (h / 2)) + dualScreenTop;
    var newWindow = window.open(url, title, 'scrollbars=yes,menubar=no,status=no,toolbar=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
    //,menubar=0,resizable=0,scrollbars=0,status=0,toolbar=0
    // Puts focus on the newWindow
    if (window.focus) {
        newWindow.focus();
    }
}
