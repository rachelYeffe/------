const urlTasks = '/MyTasks';
const urlAdmin = '/MyUsers';
let tasking = [];
const token = localStorage.getItem("token");
function getItems() {
    fetch(urlTasks)
        .then(response => response.json())
        .then(data => _displayItems(data))
        .catch(error => console.error('Unable to get items.', error));
}

function addItem() {
    const addNameTextbox = document.getElementById('add-name');
    const addIsDone = document.getElementById('add-IsDone');
console.log(addIsDone.checked);
    const item = {
        Id:1,
        IsDone:addIsDone.checked ,
        NameTasks: addNameTextbox.value.trim()
    };


    fetch(urlTasks, {
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
            addIsDone.checked=false;
        })
        .catch(error => console.error('Unable to add item.', error));
}

fetch("/Admin", {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`
    },
    body: JSON.stringify()
})
.then(response => {
    if(response.status == 200)
        {
            const allUsersLink = document.getElementById('AllUsers');
            allUsersLink.hidden = false;
 
        }
})

.catch(error => console.error('Unable to add item.', error));


function deleteItem(id) {
    fetch(`${urlTasks}/${id}`, {
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
        Id:itemId,
        IsDone: document.getElementById('edit-IsDone').checked,
        NameTasks: document.getElementById('edit-TaskName').value
    };
console.log(item.Id+" item");
    fetch(`${urlTasks}/${itemId}`, {
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
    const name = (itemCount === 1) ? 'Task' : 'Task kinds';

    document.getElementById('counter').innerText = `${itemCount} ${name}`;
}

function _displayItems(data) {
    console.log(data);
    const tBody = document.getElementById('tasks');
    tBody.innerHTML = '';

    _displayCount(data.length);

    const button = document.createElement('button');

    data.forEach(item => {
        let IsDoneCheckbox = document.createElement('input');
        IsDoneCheckbox .type = 'checkbox';
        IsDoneCheckbox .disabled = true;
        IsDoneCheckbox .checked = item.isDone;
        let editButton = button.cloneNode(false);
        editButton.innerText = 'Edit';
        editButton.setAttribute('onclick', `displayEditForm(${item.id})`);

        let deleteButton = button.cloneNode(false);
        deleteButton.innerText = 'Delete';
        deleteButton.setAttribute('onclick', `deleteItem(${item.id})`);

        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.appendChild(IsDoneCheckbox );

        let td2 = tr.insertCell(1);
        let NameTasksTextNode = document.createTextNode(item.nameTasks);
        td2.appendChild(NameTasksTextNode);
        
        let td3 = tr.insertCell(2);
        td3.appendChild(editButton);

        let td4 = tr.insertCell(3);
        td4.appendChild(deleteButton);
    });

    tasking = data;
}