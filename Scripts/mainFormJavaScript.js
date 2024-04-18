document.addEventListener("DOMContentLoaded", function () {
    
    var emptyRow = document.getElementById("emptyRow");

    
    handleEmptyRow(emptyRow);
    sortTable(0);

    var searchbar = document.getElementById("searchbar");
    var clearbtn = document.getElementById("btn-clearsearch");
    clearbtn.classList.add("disabled");
    document.getElementById("btnsearch").onclick = function () {
        if (searchbar.value.length > 0) {
            filterRowsByData(searchbar.value);
        }
    }


    searchbar.addEventListener("input", function (event) {
        clearbtn.classList.remove("disabled");
        if (searchbar.value.length >= 3) {
            filterRowsByData(searchbar.value);
        }
    });


    clearbtn.addEventListener("click", function () {
        if (!clearbtn.classList.contains("disabled")) {
            location.reload(true);
        }
    });


    var filterboxes = document.querySelectorAll(".filterbox");
    filterboxes.forEach(function (filterbox, index) {
        var clearButton = filterbox.nextElementSibling;
        filterbox.addEventListener("input", function () {
            clearbtn.classList.remove("disabled");
            clearButton.style.display = this.value ? "inline-block" : "none";
            filterrows(this, index);

        });

        clearButton.addEventListener("click", function () {
            filterbox.value = "";
            this.style.display = "none";
            showAllRows();
            if (hidecolumnwithfilters()) {
                clearbtn.classList.add("disabled");
            }

        });
    });
});

function showAllRows() {
    var table = document.getElementById("myTable");
    var rows = table.getElementsByClassName("userdata-row");
    for (var i = 0; i < rows.length; i++) {
        rows[i].style.display = "";
    }
}

function hidecolumnwithfilters() {
    var filterboxes = document.querySelectorAll(".filterbox");
    var allfilterremoved = true;
    filterboxes.forEach(function (filterbox, index) {
        var columnIndex = index;
        var data = filterbox.value.trim();
        if (data) {
            filterrows(filterbox, columnIndex);
            allfilterremoved = false;
        }
    });
    return allfilterremoved;

}


function openModal(htmlpage) {

    document.getElementById("overlay").style.display = "block";

    var width = (40 / 100) * window.innerWidth;
    var height = (95 / 100) * window.innerHeight;

    // Open the new webpage in a modal dialog
    var modalWindow = window.open(htmlpage, '_blank', 'toolbar=no,location=no,status=no,menubar=no,scrollbars=no,resizable=no,width=' + width + ',height=' + height + '');

    // Focus on the modal window
    modalWindow.focus();

    // Check if the modal window is closed
    var checkModalClosed = setInterval(function () {
        if (modalWindow.closed) {
            document.getElementById("overlay").style.display = "none";
            clearInterval(checkModalClosed);
            location.reload(true);
        }
    }, 1000);
}

function deleterecord() {
    var del = new XMLHttpRequest();
    del.open('POST', '/Home/DeleteRecords', true);
    del.onreadystatechange = function () {
        if (del.readyState === XMLHttpRequest.DONE) {
            if (del.status === 200) {
                // Handle success if needed
                location.reload(true);
                console.log("record deleted.");
            } else {
                // Handle error if needed
                console.log("record Not deleted.");
            }
        }
    };
    del.send();
}

function handleEmptyRow(row) {
    var tableRows = document.querySelectorAll("#myTable tbody tr");
    tableRows.forEach(function (row) {
        row.style.backgroundColor = "";
    });

    
    row.style.backgroundColor = "darkgrey";

    var newbtn = document.getElementById("btn-new");
    var updatebtn = document.getElementById("btn-update");
    var deletebtn = document.getElementById("btn-delete");

    if (!updatebtn.classList.contains("disabled")) {
        updatebtn.classList.add("disabled");
    }

    if (!deletebtn.classList.contains("disabled")) {
        deletebtn.classList.add("disabled");
    }
    if (newbtn.classList.contains("disabled")) {
        newbtn.classList.remove("disabled");
    }


    newbtn.onclick = function () {
        openModal(updateFormUrl + '?formtype=New');

    };
    updatebtn.onclick = function () {

    };

    deletebtn.onclick = function () {
    }
}

function handleClick(row) {
    var newbtn = document.getElementById("btn-new");
    var updatebtn = document.getElementById("btn-update");
    var deletebtn = document.getElementById("btn-delete");

    // Remove background color from previously clicked rows
    var tableRows = document.querySelectorAll("#myTable tbody tr");
    tableRows.forEach(function (row) {
        row.style.backgroundColor = "";
    });

    // Change background color of clicked row to blue
    row.style.backgroundColor = "darkgrey";

    if (updatebtn.classList.contains("disabled")) {
        updatebtn.classList.remove("disabled");
    }

    if (deletebtn.classList.contains("disabled")) {
        deletebtn.classList.remove("disabled");
    }
    if (!newbtn.classList.contains("disabled")) {
        newbtn.classList.add("disabled");
    }

    newbtn.onclick = function () {
    };
    updatebtn.onclick = function () {
        openModal(updateFormUrl + '?formtype=Update');
    };

    deletebtn.onclick = function () {
        deleterecord();
    }

    if (row.cells != null) {
        const rowIndex = row.cells[0]; // This selects the cell element itself

        // To get the value of the cell:
        const cellValue = rowIndex.innerText; // or rowIndex.textContent;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Home/StoreRowIndex', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    // Handle success if needed
                    console.log("Row index stored successfully.");
                } else {
                    // Handle error if needed
                    console.log("Error storing row index.");
                }
            }
        };
        xhr.send(JSON.stringify({ rowIndex: cellValue }));
    }

}

function handleDoubleClick(row) {
    // Handle double click event
    if (row.cells != null) {
        const rowIndex = row.cells[0]; // This selects the cell element itself

        // To get the value of the cell:
        const cellValue = rowIndex.innerText; // or rowIndex.textContent;

        var xhr = new XMLHttpRequest();
        xhr.open('POST', '/Home/StoreRowIndex', true);
        xhr.setRequestHeader('Content-Type', 'application/json');
        xhr.onreadystatechange = function () {
            if (xhr.readyState === XMLHttpRequest.DONE) {
                if (xhr.status === 200) {
                    // Handle success if needed
                    console.log("Row index stored successfully.");
                    // Open modal after storing row index
                    openModal(updateFormUrl);
                } else {
                    // Handle error if needed
                    console.log("Error storing row index.");
                }
            }
        };
        xhr.send(JSON.stringify({ rowIndex: cellValue }));
    }
}


function sortTable(columnIndex) {

    var tr = document.getElementById("headerrow");

    // Accessing the td elements based on index
    var datanode = tr.getElementsByTagName("th")[columnIndex];
    var ascedingmode = true;
    if (datanode.hasAttribute("sort")) {
        if (datanode.getAttribute("sort") === "asc") {
            ascedingmode = false;
            datanode.setAttribute("sort", "desc");
            // Handle sorting logic for ascending order
        } else if (datanode.getAttribute("sort") === "desc") {
            datanode.setAttribute("sort", "asc");
            // Handle sorting logic for descending order
        }
    } else {
        datanode.setAttribute("sort", "asc"); // Set the "sort" attribute to "asc" for initial sorting
    }


    var table, rows, switching, i, x, y, shouldSwitch;
    table = document.getElementById("myTable");
    switching = true;
    while (switching) {
        switching = false;
        rows = table.getElementsByClassName("userdata-row");
        for (i = 0; i < (rows.length - 1); i++) {
            shouldSwitch = false;
            x = rows[i].getElementsByTagName("td")[columnIndex];
            y = rows[i + 1].getElementsByTagName("td")[columnIndex];

            // Perform sorting based on the ascendingMode variable
            var comparison = ascedingmode ? x.innerHTML.toLowerCase() > y.innerHTML.toLowerCase() : x.innerHTML.toLowerCase() < y.innerHTML.toLowerCase();

            if (x && y && comparison) {
                shouldSwitch = true;
                break;
            }
        }
        if (shouldSwitch) {
            rows[i].parentNode.insertBefore(rows[i + 1], rows[i]);
            switching = true;
        }

    }
}

function filterRowsByData(data) {
    var table = document.getElementById("myTable");
    var rows = table.getElementsByClassName("userdata-row");

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var cells = row.getElementsByTagName("td");
        var matchFound = false;

        for (var j = 0; j < cells.length; j++) {
            var cellValue = cells[j].textContent || cells[j].innerText;
            if (cellValue.toLowerCase().includes(data.toLowerCase())) {
                matchFound = true;
                break;
            }
        }

        if (matchFound) {
            row.style.display = ""; // Show the row
        } else {
            row.style.display = "none"; // Hide the row
        }
    }
}

function filterrows(filterbox, columnindex) {
    var data = filterbox.value;
    var table = document.getElementById("myTable");
    var rows = table.getElementsByClassName("userdata-row");

    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var cells = row.getElementsByTagName("td");
        var cellValue = cells[columnindex].innerText;
        var matchFound = false;

        if (cellValue.toLowerCase().includes(data.toLowerCase()) && row.style.display != "none") {
            matchFound = true;
        }

        if (matchFound) {
            row.style.display = ""; 
        } else {
            row.style.display = "none"; 
        }
    }
}

