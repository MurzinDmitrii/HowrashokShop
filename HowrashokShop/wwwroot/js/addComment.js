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
document.getElementById("commentButton").addEventListener("click", async e => {
    e.preventDefault();
    var login = await CheckToken();
    if (login == true) {
        let mark = 5
        if (document.getElementById("one").checked) {
            mark = 1;
        }
        if (document.getElementById("two").checked) {
            mark = 2;
        }
        if (document.getElementById("three").checked) {
            mark = 3;
        }
        if (document.getElementById("four").checked) {
            mark = 4;
        }
        var str = "/Comments/Created?user=" + userlogin + "&id=" + document.getElementById("ProductBox").value +
            "&comment1=" + document.getElementById("CommentBox").value + "&mark=" + mark;
        location.href = str
    }
    else {
        var str = "/Auth";
        location.href = str
    }
});