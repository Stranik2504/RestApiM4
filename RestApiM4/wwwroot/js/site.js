function getItems() {
    fetch("api/TodoItems")
        .then(reponse => reponse.json())
        .then(data => displayTodoItems(data))
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