const uri = 'api/Tasks';
let tasks = [];

// 1. Отримання всіх завдань (GET)
function getTasks() {
    fetch(uri)
        .then(response => response.json())
        .then(data => _displayTasks(data))
        .catch(error => console.error('Помилка отримання даних:', error));
}

// 2. Створення нового завдання (POST)
function addTask() {
    const titleInput = document.getElementById('add-title');
    const deadlineInput = document.getElementById('add-deadline');
    const categoryInput = document.getElementById('add-category');

    const task = {
        title: titleInput.value.trim(),
        deadline: deadlineInput.value,
        categoryId: parseInt(categoryInput.value.trim(), 10) || 1
    };

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(task)
    })
        .then(response => {
            // Перевіряємо нашу унікальну логіку з Етапу 2.2
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text) });
            }
            return response.json();
        })
        .then(() => {
            titleInput.value = '';
            deadlineInput.value = '';
            categoryInput.value = '';
            getTasks(); // Оновлюємо таблицю
        })
        .catch(error => {
            alert('Помилка API: ' + error.message);
            console.error('Не вдалося додати:', error);
        });
}

// 3. Видалення завдання (DELETE)
function deleteTask(id) {
    fetch(`${uri}/${id}`, {
        method: 'DELETE'
    })
        .then(() => getTasks())
        .catch(error => console.error('Помилка видалення:', error));
}

// Функція для малювання таблиці
function _displayTasks(data) {
    const tBody = document.getElementById('tasks-body');
    tBody.innerHTML = '';

    data.forEach(task => {
        let tr = tBody.insertRow();

        let td1 = tr.insertCell(0);
        td1.textContent = task.title;

        let td2 = tr.insertCell(1);
        let date = new Date(task.deadline);
        td2.textContent = date.toLocaleString('uk-UA'); // Форматуємо дату

        let td3 = tr.insertCell(2);
        td3.textContent = task.isCompleted ? 'Виконано' : 'В процесі';

        let td4 = tr.insertCell(3);
        let deleteButton = document.createElement('button');
        deleteButton.textContent = 'Видалити';
        deleteButton.className = 'btn-danger';
        deleteButton.onclick = function () { deleteTask(task.id); };
        td4.appendChild(deleteButton);
    });
    tasks = data;
}

// Завантажуємо завдання при відкритті сторінки
getTasks();