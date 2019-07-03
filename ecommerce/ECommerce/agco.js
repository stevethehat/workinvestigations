function creditCheck() {
    const url = getCustomerUrl() + "/credit-check?amount=" + getValue("amount") + "&currency=" + getValue("currency");
    get(url);
}

function priceCheck() {
    const url = getCustomerUrl() + "/price-check";
    const data = {
        "items": []
    };
    post(url, data);
}

function makeOrder() {
    alert("make order");
}

function getCustomerUrl() {
    return "/api/v1/manufacturer/agco/customer/" + getValue("customer");
}

document.partCount = 0;
function setupParts() {
    addPart();
}

function addExtraPart() {
    addPart();
}

function addPart() {
    document.partCount++;
    const parts = document.getElementById("parts");
    const part = document.createElement("div");

    part.setAttribute("class", "part");

    addInput(part, "partId" + document.partCount, "Part ID");
    addInput(part, "quantity" + document.partCount, "Quantity");
    parts.appendChild(part);
}

function addInput(parent, name, text) {
    const label = document.createElement("label");
    const input = document.createElement("input");
    
    parent.appendChild(label);
    parent.appendChild(input);

    label.innerText = text;
    input.id = name;
    input.name = name;
}

function getValue(id) {
    const input = document.getElementById(id);
    return input.value;
}

function get(url) {
    alert("get " + url);
}

function post(url, data) {
    debugger;
    const xhr = new XMLHttpRequest();

    xhr.open('POST', url);
    xhr.setRequestHeader('Content-Type', 'application/json');
    xhr.onload = function() {
        if (xhr.status === 200 && xhr.responseText !== newName) {
            alert('Something went wrong.  Name is now ' + xhr.responseText);
        }
        else if (xhr.status !== 200) {
            alert('Request failed.  Returned status of ' + xhr.status);
        }
    };
    xhr.send(data);    
}