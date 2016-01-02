<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, maximum-scale=1" />
    <title>Your Hostname</title>
    <style type="text/css">
        body {
            background-color: black;
            color: white;
            font-size: 0.75em;
            font-family: "Century Gothic",CenturyGothic,AppleGothic,sans-serif;
            text-align: center;
        }

        .main {
            background-color: #003a4f;
            margin-left: auto;
            margin-right: auto;
            margin-top: 20px;
            margin-bottom: 10px;
            max-width: 700px;
            width: auto;
            padding: 10px 20px 20px 20px;
            text-align: left;
        }

        .versionLabel {
            font-size: 0.75em;
            color: #7f9ca7;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: "Century Gothic",CenturyGothic,AppleGothic,sans-serif;
            font-variant: small-caps;
            font-weight: 400;
            margin-bottom: 5px;
            margin-top: 40px;
        }

        h1 {
            margin-top: 0px;
            text-align: right;
        }

        h3 {
            margin-top: 0px;
            margin-bottom: 0px;
            margin-left: 15px;
            font-size: medium;
        }

        hr {
            border: none;
            height: 1px;
            background-color: red;
        }

        p, ul {
            margin-left: 30px;
            margin-top: 5px;
            font-size: small;
            line-height: 1.5;
        }

        ul {
            margin-left: initial;
        }

        a {
            color: lightcyan;
        }

            a:hover {
                text-decoration: none;
            }
    </style>
    <asp:Literal runat="server" ID="IPv4Javascript">
        <script src="http://ipv4.hostname.jmay.us/ipv4js.ashx" defer="defer"></script>
    </asp:Literal>
</head>
<body>
    <form id="form1" runat="server">
        <div class="main">
            <h1>Hostname+</h1>
            <hr />
            <h2>Hostname</h2>
            <p>
                <asp:Label runat="server" ID="Hostname" />
                <span id="IPv4Hostname"></span>
            </p>
            <h2>IP</h2>
            <p>
                <asp:Label runat="server" ID="IPLabel" />
                <span id="IPv4Address"></span>
            </p>
            <h2>User Agent</h2>
            <p>
                <asp:Label runat="server" ID="UserAgent" />
            </p>
        </div>
        &copy;2015-2016 Joel May
    </form>
</body>
</html>
