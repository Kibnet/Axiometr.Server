
function show_element(e){
	
	
	(async () => {
		let ur=btoa(window.document.URL);
		const resp = await fetch("https://25.34.118.131:5001/pageprofile?format=json&pageaddress="+ur);
		const jsonData = await resp.json();
		console.log("jsonData: ", jsonData);

if (jsonData.perks.length>0){
		let res="";
		for (let i = 0; i < jsonData.perks.length; i++) { 
			let icon = "<img style='width:48px;height:48px' src='"+jsonData.perks[i].imagePath+"' title='"+jsonData.perks[i].name+"'></img>";
			res=res+icon;
		  }
		let wit = (jsonData.perks.length * 48 + 12) * -0.5;
		let elem = '<div id="popup" style="height:60px;color:#555;border-radius:10px;;border:1px solid #f2c779;	background:#fff8c4;box-shadow: 0 0 10px rgba(0,0,0,0.5); padding:6px; margin-top:6px;	position:fixed;	top: 0%;left: 50%; z-index:999999; margin-left:'+wit+'px">'+           
				
				
				
				res+
	
				
				'</div>';
				
		
		let node = document.createElement("div");
		node.innerHTML = elem;
		document.body.appendChild(node);
		
		setTimeout(node.remove, 5000); //скрыть панель

		}
	 })();


	//var metki=window.document.URL;
	
}

function aks(){
	let elem2 = '<div id="ask" style="height:60px;color:#555;border-radius:10px;;border:1px solid #f2c779;	background:#fff8c4;box-shadow: 0 0 10px rgba(0,0,0,0.5); padding:6px; margin-top:6px;	position:fixed;	bottom: 1%;left: 50%; margin-left:-150px;z-index:999999; ">Как автор статьи оценивает право на аборт?<br/><button>\Положительно</button><button>Отрицательно</button><button>Не знаю</button></div>';
	

	let node2 = document.createElement("div");
	node2.innerHTML = elem2;
	document.body.appendChild(node2);
}

function getFocus(){
	window.addEventListener('load',show_element);

	//setTimeout(ask, 60000);
	
}
getFocus();
aks();




	

