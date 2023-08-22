var executionTime = undefined
var executionTimeTotal = undefined
var intervalTimer = undefined
var timeInSeconds = undefined

$(document).ready(function () {
    init()
})
function init(e) {
    const task = getCookie('executingTask')
    if (task !== undefined) {
        const taskRegistry = getCookie('taskRegistry')
        if (taskRegistry !== undefined) {
            $('img[name="playOrStop"]').toArray().forEach((item) => {
                if (item.id === `task_${task}`) {
                    item.src = '/img/stop_time.png'
                    item.title = 'Detener tarea'
                    item.onclick = () => { stopTask(taskRegistry) }
                } else {
                    item.remove()
                }
            })
            $.ajax({
                type: 'get',
                url: `/project/task/executionDate/${taskRegistry}`,
                success: (res) => {
                    executionTime = new Date(res['executionDate'])
                    executionTimeTotal = res['executionTime']
                    let eventGrid = false
                    if (e !== undefined) {
                        eventGrid = true
                    }
                    showTime(res['taskID'], taskRegistry,eventGrid)
                }
            })
        }
    }
    else {
        $('img[name="playOrStop"]').toArray().forEach((item) => {
            item.src = '/img/play_time.png'
            item.title = 'Iniciar tarea'
            var taskId = $(item).attr("id").split("_")[1]
            item.onclick = () => { playTask(taskId) }
        })
    }
}
function updateStatus(taskID, taskStatusID) {
    $.ajax({
        type: 'put',
        url: `/project/task/updateStatus/${taskID}/${taskStatusID}`,
        success: function (res) {
            DevExpress.ui.notify("Actualizado con exito", "success");
        },
        error: function () {
            DevExpress.ui.notify("Hubo un error actualizando el estado de la tarea.", "error");
        }
    })
}
function playTask(taskID) {
    const image = $('#task_' + taskID)
    $(image[0]).unbind("click")
    $.ajax({
        type: 'post',
        url: `/project/task/play/${taskID}`,
        success: function (res) {
            var images = $('img[title="Iniciar tarea"]').toArray()
            const position = images.indexOf(image[0])
            images.splice(position, 1)
            images.forEach((x) => { x.remove() })
            image[0].src = '/img/stop_time.png'
            image[0].title = 'Detener tarea'
            $(image[0]).unbind("click")
            image[0].onclick = () => { stopTask(res['id']) }
            executionTime = new Date(res['executionDate'])
            executionTimeTotal = res['executionTime']
            showTime(taskID, res['id'])
        },
        error: function () {
            image[0].onclick = () => { playTask(taskID) }
            DevExpress.ui.notify("Hubo un error iniciando la tarea", "error");
        }
    })
}
function stopTask(taskRegistryID) {
    clearInterval(intervalTimer)
    $.ajax({
        type: 'post',
        url: `/project/task/stop/${taskRegistryID}/${timeInSeconds}`,
        success: function (res) {
            document.location.reload()
        },
        error: function () {
            DevExpress.ui.notify("Hubo un error deteniendo la tarea", "error")
        }
    })
}
function showTime(taskID, taskRegistryID, eventGrid = false) {
    if (eventGrid != true) {
        $('#div_crono_2').toggleClass('d-none')
    }
    var nameProject = 'Nombre proyecto'
    var nameTask = 'Nombre tarea'
    $.ajax({
        type: 'get',
        url: '/project/task/get/' + taskID,
        success: (res) => {
            nameTask = res['TaskName']
            nameProject = res['ProjectName']
        }
    })
    intervalTimer = setInterval(function () {
        const timeInGrid = $(`#div_crono_1_${taskID}`)
        var timeinMillis = Date.now() - executionTime
        timeInSeconds = Math.floor(timeinMillis / 1000) + executionTimeTotal
        var time = formatTime(timeInSeconds)
        var plantilla = ''
        if (timeInGrid !== undefined) {
            plantilla = `<span class='badge bg-success text-light p-1 mt-1'>${time}</span>`
            timeInGrid.html(plantilla)
        }

        const timeInHeader = $('#div_crono_2')
        if (timeInHeader !== undefined) {
            var actualTime = formatTime(Math.floor(timeinMillis / 1000))
            plantilla = `
            			<div>
                            <span title='Proyecto: ${nameProject}'>${nameTask}</span>
                            <br />
                            <span style='font-weight:bold;'>Actual: ${actualTime}</span>&nbsp;&nbsp;
                            <span style='font-weight:bold;'>Total: ${time}</span>
                        </div>
                        <img onclick='stopTask("${taskRegistryID}")' src='/img/stop_time.png' style='width: 20px;height: 20px;margin-left: 10px;cursor:pointer;' title='Detener Tarea' />
            `
            timeInHeader.html(plantilla)
        }
        const timeInHistory = $(`#div_crono_3_${taskID}`)
        if (timeInHistory !== undefined) {
            timeInHistory.text(time)
        }
    }, 1000)
}
function formatTime(timeInSeconds) {
    var hours = Math.floor(timeInSeconds / 3600);
    var minutes = Math.floor((timeInSeconds % 3600) / 60);
    var seconds = timeInSeconds % 60;
    var formattedTime = "";
    if (hours > 0) {
        formattedTime = `${hours.toString().padStart(2, '0')}:${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')} horas`;
    } else if (minutes > 0) {
        formattedTime = `${minutes.toString().padStart(2, '0')}:${seconds.toString().padStart(2, '0')} min`;
    } else {
        formattedTime = `${seconds.toString().padStart(2, '0')} seg`;
    }
    return formattedTime;
}
function getCookie(cname) {
    let name = cname + "=";
    let ca = document.cookie.split(';');
    for (let i = 0; i < ca.length; i++) {
        let c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return undefined;
}