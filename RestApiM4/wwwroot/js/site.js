const uri = "api/TodoItems";

function getItems() {
    fetch(uri)
        .then(reponse => reponse.json())
        .then(data => displayTodoItems(data))
        .catch(error => console.error(error))
}

function createItem() {
    const name = document.getElementById("name-todo");
    const isComplete = document.getElementById("is_complete-todo");

    const item = {
        name: name,
        isComplete: isComplete
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => {
            if (!response.ok) {
                console.error(response.statusText);
                return;
            }

            document.location = "/index.html";
        })
        .catch(error => console.error(error))
}

function displayTodoItems(data) {
    const tBody = document.getElementById("todo");
    tBody.innerHTML = "";

    data.forEach(item => {
        let tr = tBody.insertRow();

        // Name
        let textNode = document.createTextNode(item.name);
        tr.insertCell(0).appendChild(textNode);

        // Is completed
        let checkBox = document.createElement('input');
        checkBox.type = 'checkbox';
        checkBox.disabled = true;
        checkBox.checked = item.isComplete;
        tr.insertCell(1).appendChild(checkBox);
    });
}