$(() => {
    const connection = new signalR.HubConnectionBuilder()
        .withUrl("/taskHub").build();

    connection.start()

    $('#add-task-button').on('click', function () {
        const title = $('#title').val();
        $('#title').val('');
        connection.invoke("AddNewTask", { title })
    });

    connection.on("AddTask", (task) => {
        $('#tasks').append(`<tr id="task-${task.id}"><td>${task.title}</td><td><button class="btn btn-info" data-id="${task.id}">I'm doing this one!</button></td></tr>`)
    });

    $('#tasks').on('click', '.btn-info', function () {
        const id = $(this).data('id');
        connection.invoke("UpdateStatus", { id, status: 1 })
    });

    $('#tasks').on('click', '.btn-success', function () {
        const id = $(this).data('id');
        connection.invoke("UpdateStatus", { id, status: 2 })
    });

    connection.on("RemoveTask", (taskId) => {
        $(`#task-${taskId}`).remove();
    });

    connection.on("MyTask", (task) => {
        console.log("hello");
        $(`#task-${task.id}`).find('td:eq(1)').html(`<td><button class="btn btn-success" data-id="${task.id}">I'm done!</button></td>`);
    });

    connection.on("UpdateTask", (task) => {
        console.log("hello");
        $(`#task-${task.id}`).find('td:eq(1)').html(`<td><button class="btn btn-warning" disabled>${task.user.name} is working on this</button></td>`);
    });
});