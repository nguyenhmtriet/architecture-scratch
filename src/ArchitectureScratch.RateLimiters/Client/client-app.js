const url = 'http://localhost:50003';
let clock;

function updateClock() {
  const now = new Date();
  const hours = String(now.getHours()).padStart(2, "0");
  const minutes = String(now.getMinutes()).padStart(2, "0");
  const seconds = String(now.getSeconds()).padStart(2, "0");
  const time = `${hours}:${minutes}:${seconds}`;
  clock.textContent = time;
}

function createButton(numberOfRequests) {
  const button = document.createElement("button");
  const results = document.getElementById("results");
  button.textContent = "Call GET " + numberOfRequests + " Requests";
  button.addEventListener("click", () => {
    results.innerHTML = "";

    for (let index = 0; index < numberOfRequests; index++) {
      fetch(url + "/")
          .then((response) => response.json())
          .then((data) => {
            // Handle the response data here
            console.log(data);

            results.appendChild(document.createElement("div")).innerText = JSON.stringify(data) + ' ' + '#' + (index + 1);
          })
          .catch((error) => {
            // Handle any errors that occur during the request
            console.error(error);
          });
    }
  });
  
  return button;
}

document.addEventListener("DOMContentLoaded", () => {
  clock = document.getElementById("clock");
  document.body.appendChild(clock);

  setInterval(updateClock, 1000);
  let button = createButton(20);
  document.body.appendChild(button);
  
  button = createButton(30);
  document.body.appendChild(button);
  
  button = createButton(40);
  document.body.appendChild(button);

  button = createButton(20);
  document.body.appendChild(button);
  
})