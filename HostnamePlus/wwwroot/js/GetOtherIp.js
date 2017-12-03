var req = new XMLHttpRequest();
req.onreadystatechange = handleReply;
req.responseType = "json";
req.open("GET", otherIpAPIURL, true);
req.send();

function handleReply() {
    if (this.readyState == 4
        && this.status == 200) {

        var otherIpInfo = this.response;
        var prefix = "<br /><span class='versionLabel'>" + otherIpType + ": </span>";
        var addressInnnerHtml = prefix + otherIpInfo.ipAddress;
        var hostNameInnerHtml = prefix + otherIpInfo.hostName;
        document.getElementById("OtherAddress").innerHTML = addressInnnerHtml;
        document.getElementById("OtherHostname").innerHTML = hostNameInnerHtml;
    }
}