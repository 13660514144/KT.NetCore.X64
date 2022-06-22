//获取大厦
function edifices() {
    var url
}
function CreatHead(O) {
	PostHeader = {
		timestamp: parseInt((new Date()).valueOf() / 1000),
		nonce: guid(),
		token: Token,
		platform: "PC",
		version: "1.0.0",
		secret: Secret,
		sign: ""
	};
	var md5 = 'nonce=' + PostHeader.nonce + '&platform=PC&timestamp=' + PostHeader.timestamp + '&token=' + Token + '&version=1.0.0&secret=' + Secret;
	var md5str = hex_md5(md5);
	PostHeader.sign = md5str.toLowerCase();
	return PostHeader;
}