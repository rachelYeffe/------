const urlUsers = '/MyUsers';
let users = [];
const token = localStorage.getItem('token')
function getUsers() {
    fetch(urlUsers, {
        method: 'GET',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify()
    })
        .then(response =>
            response.json())
        .then(data => _displayUsers(data))
        .catch(error => console.error('Unable to get items.', error));
}


function addUser() {
    const UserName = document.getElementById('add-UserName');
    const IsAdmin= document.getElementById('add-IsAdmin');
    const Password = document.getElementById('add-IsAdmin');
    // const addIsDone = document.getElementById('add-IsDone');

    // console.log(addIsDone.checked);
    const user = {
        Id: 0,
        UserName: UserName.value,
        IsAdmin: IsAdmin.value?1:0,
        Password: Password.value
    };


    fetch(urlUsers, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`

        },
        body: JSON.stringify(user)
    })
        .then(response => response.json())
        .then(() => {
            getUsers();
            UserName.value = '';
            IsAdmin.value = '';
            Password.value='';
        })
        .catch(error => console.error('Unable to add user.', error));
}

function deleteUser(id) {
    fetch(`${urlUsers}/${id}`, {
        method: 'DELETE',
        headers: {
            'Authorization': `Bearer ${token}`

        },
    })
        .then(() => getUsers())
        .catch(error => console.error('Unable to delete user.', error));
}

function displayEditForm(id) {
    const user = users.find(user => user.id === id);

    // document.getElementById('edit-UserName').value = user.userName;
    document.getElementById('edit-Id').value = user.id;
    // document.getElementById('edit-IsAdmin').value = user.isAdmin;
    document.getElementById('editForm').style.display = 'block';
}

function updateUser() {
    const userId = document.getElementById('edit-Id').value;
    const userName = document.getElementById('edit-UserName').value;
    const isAdmin = parseInt(document.getElementById('edit-IsAdmin').value);

    const user = {
        Id: userId,
        UserName: userName,
        IsAdmin: isAdmin?1:0,
        Password: 0
    };

    fetch(`${urlUsers}/${userId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
        },
        body: JSON.stringify(user)
    })
        .then(response => {
            if (response.status == 200) {
                getUsers();
            } else {
                console.error('Update request failed with status:', response.status);
            }
        })
        .catch(error => console.error('Unable to update user.', error));

    closeInput();
    return false;
}


function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(userCount) {
    const name = (userCount === 1) ? 'Users' : 'User kinds';

    document.getElementById('counter').innerText = `${userCount} ${name}`;
}

function _displayUsers(data) {
    console.log(data);
    const tBody = document.getElementById('users');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        // let Id = document.createElement('input');
        // Id .type = 'text';
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteUser(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let idTextNode = document.createTextNode(item.id);
        td1.appendChild(idTextNode);

        let td2 = tr.insertCell(1);
        let userNameTextNode = document.createTextNode(item.userName);
        td2.appendChild(userNameTextNode);

        let td3 = tr.insertCell(2);
        let userTypeTextNode = document.createTextNode(item.isAdmin);
        td3.appendChild(userTypeTextNode);

        let td4 = tr.insertCell(3);
        let passwordTextNode = document.createTextNode(item.password);
        td4.appendChild(passwordTextNode);

        let td5 = tr.insertCell(4);
        td5.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td6.appendChild(deleteButton);
    });

    users = data;
}