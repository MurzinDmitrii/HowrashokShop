var tokenKey = "accessToken";
var login = "userlogin";

async function CheckToken(e) {
    e?.preventDefault();
    // �������� ����� �� sessionStorage
    const token = localStorage.getItem(tokenKey);
    const userlogin = localStorage.getItem(login);
    // ���������� ������ � "/data
    const response = await fetch("/Profile", {
        method: "GET",
        headers: {
            "Accept": "application/json",
            "Authorization": "Bearer " + token  // �������� ������ � ���������
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
// ��� ������� �� ������ �������� ����� ���� ������ � /login ��� ��������� ������
document.getElementById("submitLogin").addEventListener("click", async e => {
    e.preventDefault();
    // ���������� ������ � �������� �����
    const response = await fetch("/login", {
        method: "POST",
        headers: { "Accept": "application/json", "Content-Type": "application/json" },
        body: JSON.stringify({
            Email: document.getElementById("login").value,
            Password: document.getElementById("password").value
        })
    });
    // ���� ������ ������ ���������
    if (response.ok === true) {
        // �������� ������
        const data = await response.json();
        // ��������� � ��������� sessionStorage ����� �������
        localStorage.setItem(tokenKey, data.access_token);
        localStorage.setItem('userlogin', data.username);
        CheckToken();
    }
    else  // ���� ��������� ������, �������� ��� �������
        console.log("Status: ", response.status);
});

//// ������ ��� ��������� �� ���� "/data" ��� ��������� ������
//document.getElementById("getData").addEventListener("click", CheckToken);

//// �������� ����� - ������ ������� ����� � ������ ��������� ������
//document.getElementById("logOut").addEventListener("click", e => {

//    e.preventDefault();
//    document.getElementById("userName").innerText = "";
//    document.getElementById("userInfo").style.display = "none";
//    document.getElementById("loginForm").style.display = "block";
//    localStorage.removeItem(tokenKey);
//    localStorage.removeItem(userlogin);
//});