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
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}


function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addIsDone = document.getElementById('add-IsDone');
    console.log(addIsDone.checked);
    const item = {
        Id: 0,
        UserName: "",
        IsAdmin: 0,
        Password: 0
    };


    fetch(urlUsers, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(() => {
            getItems();
            addNameTextbox.value = '';
            addIsDone.checked = false;
        })
        .catch(error => console.error('Unable to add item.', error));
}

function deleteItem(id) {
    fetch(`${urlUsers}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to delete item.', error));
}

function displayEditForm(id) {
    const item = tasking.find(item => item.id === id);

    document.getElementById('edit-TaskName').value = item.nameTasks;
    document.getElementById('edit-Id').value = item.id;
    document.getElementById('edit-IsDone').checked = item.isDone;
    document.getElementById('editForm').style.display = 'block';
}

function updateItem() {
    const itemId = document.getElementById('edit-Id').value;
    console.log(itemId);
    const item = {
        Id: itemId,
        IsDone: document.getElementById('edit-IsDone').checked,
        NameTasks: document.getElementById('edit-TaskName').value
    };
    console.log(item.Id + " item");
    fetch(`${urlUsers}/${itemId}`, {
        method: 'PUT',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(() => getItems())
        .catch(error => console.error('Unable to update item.', error));

    closeInput();

    return false;
}

function closeInput() {
    document.getElementById('editForm').style.display = 'none';
}

function _displayCount(itemCount) {
    const name = (itemCount === 1) ? 'Users' : 'User kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
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
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        let idTextNode = document.createTextNode(item.id);
        td1.appendChild(idTextNode);

        let td2 = tr.insertCell(1);
        let userNameTextNode = document.createTextNode(item.userName);
        td2.appendChild(userNameTextNode);

        let td3 = tr.insertCell(2);
        let userTypeTextNode = document.createTextNode(item.userType);
        td2.appendChild(userTypeTextNode);

        let td4 = tr.insertCell(3);
        let passwordTextNode = document.createTextNode(item.password);
        td2.appendChild(passwordTextNode);

        let td5 = tr.insertCell(4);
        td3.appendChild(editButton);

        let td6 = tr.insertCell(5);
        td4.appendChild(deleteButton);
    });

    tasking = data;
}