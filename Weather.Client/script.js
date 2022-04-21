let url = "https://localhost:44386/Weather/";
let xhttp = new XMLHttpRequest();
let sities;
xhttp.onreadystatechange = function(){
	if(this.readyState == 4 && this.status == 200)
	{
		SetSities(this.responseText);
	}
}
xhttp.open("GET", url, true)
xhttp.send();

function SetSities(data){
	 sities = JSON.parse(data);
	 var select = document.getElementById("select-cities");
	 for (var i = 0; i < sities.length; i++) {
	 	var el = document.createElement("option");
	 	el.textContent = sities[i].name;
    	el.value = sities[i].id;
    	select.appendChild(el);
	 }
}

$(".search-button").click(function GetWeather(e){
	e.preventDefault();

	var http = new XMLHttpRequest();
	http.open("GET", url + document.getElementById("select-cities").value +"/" + document.getElementById("date-time").value, true)
	http.send();
	http.onreadystatechange = function(){

		if(this.readyState == 4 && this.status == 200)
		{
		    SetWeather(this.responseText);
		}
		else
		{
			document.getElementById("result").textContent = "";
		}
	}
})

function SetWeather(data){
	var temp = JSON.parse(data);
	var tempFrom = parseInt(temp.temperatureFrom) > 0 ? "+" + temp.temperatureFrom : temp.temperatureFrom;
 	var tempTo = parseInt(temp.temperatureTo) > 0 ? "+" + temp.temperatureTo : temp.temperatureTo;

	document.getElementById("result").textContent = "Температура от : " + tempFrom 
													+ " до " + tempTo + " градусов";
}

	

