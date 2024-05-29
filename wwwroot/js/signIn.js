const ury = 'Login/';
const token = localStorage.getItem("token");
function Login()
{
    const nameTextbox = document.getElementById('name').value;
    const passwordTextbox = document.getElementById('password').value;
    Sign_in(nameTextbox,passwordTextbox)
}


function Sign_in(nameTextbox,passwordTextbox) {
 

    const item = {
        Id: 0,
        UserName: nameTextbox,
        IsAdmin: 0,
        Password: passwordTextbox,
    };

    fetch(ury, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`

        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (response.status == 200) {
                return response.json();
            } else {
                throw new Error('Failed to sign in');
            }
        })
        .then(data => {
            console.log(data + "  data");
            // nameTextbox.value = '';
            // passwordTextbox.value = '';
            document.getElementById('name').value = '';
            document.getElementById('password').value = '';
            localStorage.setItem('token', data); // Store the token in localStorage
            window.location.href = "index.html"; // Redirect to the index page
        })
        .catch(error => console.error('Unable to sign in.', error));
}
function handleCredentialResponse(response) {
    if (response.credential) {
        var idToken = response.credential;
        var decodedToken = parseJwt(idToken);
        var userName = decodedToken.name;
        var userPassword = decodedToken.sub;
        Sign_in(userName, userPassword);
    } else {
        alert('Google Sign-In was cancelled.');
    }
}


//Parses JWT token from Google Sign-In
function parseJwt(token) {
    var base64Url = token.split('.')[1];
    var base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
    var jsonPayload = decodeURIComponent(atob(base64).split('').map(function (c) {
        return '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2);
    }).join(''));

    return JSON.parse(jsonPayload);
}


