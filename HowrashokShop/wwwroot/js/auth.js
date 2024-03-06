var tokenKey = "accessToken";
var login = "userlogin";

async function CheckToken(e) {
    e?.preventDefault();
    // получаем токен из sessionStorage
    const token = localStorage.getItem(tokenKey);
    const userlogin = localStorage.getItem(login);
    // отправляем запрос к "/data
    const response = await fetch("/Profile", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token  // передача токена в заголовке
        }
    });

    if (response.ok === true) {
        var str = "/Profile/Edit?login="+userlogin
        location.href = str
    }
    else
        console.log("Status: ", response.status);
}
CheckToken();
// при нажатии на кнопку отправки формы идет запрос к /login для получения токена
document.getElementById("submitLogin").addEventListener("click", async e => {
    e.preventDefault();
    // отправляет запрос и получаем ответ
    const response = await fetch("/login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Email: document.getElementById("login").value,
            Password: document.getElementById("password").value
        })
    });
    // если запрос прошел нормально
    if (response.ok === true) {
        // получаем данные
        const data = await response.json();
        // сохраняем в хранилище sessionStorage токен доступа
        localStorage.setItem(tokenKey, data.access_token);
        localStorage.setItem('userlogin', data.username);
        CheckToken();
    }
    else  // если произошла ошибка, получаем код статуса
        console.log("Status: ", response.status);
});

//// кнопка для обращения по пути "/data" для получения данных
//document.getElementById("getData").addEventListener("click", CheckToken);

//// условный выход - просто удаляем токен и меняем видимость блоков
//document.getElementById("logOut").addEventListener("click", e => {

//    e.preventDefault();
//    document.getElementById("userName").innerText = "";
//    document.getElementById("userInfo").style.display = "none";
//    document.getElementById("loginForm").style.display = "block";
//    localStorage.removeItem(tokenKey);
//    localStorage.removeItem(userlogin);
//});