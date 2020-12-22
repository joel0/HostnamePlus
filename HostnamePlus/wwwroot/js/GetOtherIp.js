﻿var req = new XMLHttpRequest();
req.onreadystatechange = handleReply;
req.responseType = "json";
// otherIpAPIURL is a variable defined from the server-side on the HTML page.
req.open("GET", otherIpAPIURL, true);
req.send();

var cfReq = new XMLHttpRequest();
cfReq.onreadystatechange = handleCfReply;
cfReq.responseType = "json";
cfReq.open("GET", "https://hostname.jmay.xyz/api/cfIp", true);
cfReq.send();

/**
 * Handles the AJAX response
 */
function handleReply() {
    // Falure results are discarded, as this is expected to fail of non-dual-stack connections.
    if (this.readyState == 4 && this.status == 200) {
        // This object is defined in Models/IndexModel.
        var otherIpInfo = this.response;
        var prefix = "<br /><span class='versionLabel'>" + otherIpType + ": </span>";
        var addressInnnerHtml = prefix + otherIpInfo.ip + "<br />";
        var hostNameInnerHtml = prefix + otherIpInfo.hostName + "<br />";
        document.getElementById("OtherAddress").innerHTML = addressInnnerHtml;
        document.getElementById("OtherHostname").innerHTML = hostNameInnerHtml;
    }
}

function handleCfReply() {
    // Discard failure results
    if (this.readyState == 4 && this.status == 200) {
        var otherIpInfo = this.response;
        console.log("IP as reported by Cloudflare: ", otherIpInfo)
    }
}
