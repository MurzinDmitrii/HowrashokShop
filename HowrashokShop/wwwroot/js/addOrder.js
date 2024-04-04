var tokenKey = "accessToken";
var login = "userlogin";
const token = localStorage.getItem(tokenKey);
const userlogin = localStorage.getItem(login);

async function CheckToken(e) {
    e?.preventDefault();
    // отправляем запрос к "/data
    if (token != null) {
        const response = await fetch("/Profile", {
            method: "GET",
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + token  // передача токена в заголовке
            }
        });

        if (response.ok === true) {
            return true
        }
        else
            return false
    }
}
// при нажатии на кнопку отправки формы идет запрос к /login для получения токена
document.getElementById("OrderButton").addEventListener("click", async e => {
    e.preventDefault();
    var login = await CheckToken();
    if (login == true) {
        var str = "/Orders/Create?login=" + userlogin;
        location.href = str
    }
    else {
        var str = "/Auth";
        location.href = str
    }
});