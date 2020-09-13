
function show_element(e){
	
	
	(async () => {
		let ur=btoa(window.document.URL);
		const resp = await fetch("https://25.34.118.131:5001/pageprofile?format=json&pageaddress="+ur);
		const jsonData = await resp.json();
		console.log("jsonData: ", jsonData);


		let res="";
		for (let i = 0; i < jsonData.perks.length; i++) { 
			let icon = "<img align='left' style='float: left;width:48px;height:48px' src='"+jsonData.perks[i].imagePath+"' title='"+jsonData.perks[i].name+"'></img><span>"+jsonData.perks[i].name+"</span>";
			res=res+icon;
		  }
		let wit = (jsonData.perks.length * 48 + 12) * -0.5;
		let elem = '<div id="popup" >'+           
				
				
				
				res+
	
				
				'</div>';
				
		
		let node = document.createElement("div");
		node.innerHTML = elem;
		document.body.appendChild(node);
	


	 })();


	//var metki=window.document.URL;
	
}

function hide(){
	var node = document.getElementById("popup").remove()
	
}

function getFocus(){
	window.addEventListener('load',show_element);
	
}
getFocus();




	

