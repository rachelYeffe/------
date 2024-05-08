const ury = 'Login/';
function Sign_in() {
    const nameTextbox = document.getElementById('name').value;
    const passwordTextbox = document.getElementById('password').value;

    const item = {
        Id: passwordTextbox,
        UserName: nameTextbox,
        IsAdmin: 0,
        Password: passwordTextbox,
    };

    fetch(ury, {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
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