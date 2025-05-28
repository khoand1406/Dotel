showPayment();
function showPayment() {
  const url3 = "../json/Servicepost.json";
fetch(url3)
.then((response) => response.json()) // parse the JSON from the server
.then(function(post) {
// lấy id từ localstorage
var id = localStorage.getItem("ID_POST");
// tìm post có id trùng với id lấy từ localstorage
var post = post.find((post) => post.id == id);  
// gán phần tử post vào mảng mới
var detailpost = [post];

// lấy mảng mới và map ra html
var bill = detailpost.map(function(post) {
    return `<h2 class="text-primary text-center mb-3">HÓA ĐƠN GÓI DỊCH VỤ</h2>
            
    <div class="row">
    <div class="col-md-5">
    <div class="row g-6">
        <aside class="aa-properties-sidebar-payment">
        <h6>Loại bài đăng:</h6>
    </div>
    </div>
    <div class="col-md-7">
    <div class="row g-4">
        <aside class="aa-properties-sidebar-payment">
        <h6 >${post.typeService}</h6>
    </div>
    </div>
    </div>
    <br>
    <div class="row">
        <div class="col-md-5">
        <div class="row g-6">
            <aside class="aa-properties-sidebar-payment">
            <h6>Loại tin:</h6>
        </div>
        </div>
        <div class="col-md-7">
        <div class="row g-4">
            <aside class="aa-properties-sidebar-payment">
            <h6 >${post.typePost}</h6>
        </div>
        </div>
    </div>
    <br>
    <div class="row">
            <div class="col-md-5">
            <div class="row g-6">
                <aside class="aa-properties-sidebar-payment">
                <h6>Thời hạn:</h6>
            </div>
            </div>
            <div class="col-md-7">
            <div class="row g-4">
                <aside class="aa-properties-sidebar-payment">
                <h6 class="" >${post.duration}</h6>
        </div>
        </div>
    </div>
    <br>
    <div class="row">
            <div class="col-md-5">
            <div class="row g-6">
                <aside class="aa-properties-sidebar">
                <h6 class="heading2">Mệnh giá:</h6>
            </div>
            </div>
            <div class="col-md-7">
            <div class="row g-4">
                <aside class="aa-properties-sidebar">
                <h6 class="heading2" >${post.price}</h6>
        </div>
        </div>
    </div>`

})
document.getElementById("bill").innerHTML = bill;

})
}

function Details(id) {
    localStorage.setItem("ID_POST", id);
    window.location.href = "Payment.html";
    }


   
    function PostType() {
    localStorage.getItem("ID_POST");
        switch(localStorage.getItem("ID_POST")) {
            case "1":
              window.location.href = "FormRenthome.html";
              break;
            case "2":
                window.location.href = "FormRenthome.html";
              break;
              case "3":
                window.location.href = "FormRenthome.html";
              break;
              case "4":
                window.location.href = "FormRenthome.html";
              break;
              case "5":
                window.location.href = "FormRenthome.html";
              break;
              case "6":
                window.location.href = "FormRenthome.html";
              break;
              case "7":
                window.location.href = "FormRoommate.html";
              break;
              case "8":
                window.location.href = "FormRoommate.html";
              break;
              case "9":
                window.location.href = "FormRoommate.html";
              break;
              case "10":
                window.location.href = "FormRoommate.html";
              break;
              case "11":
                window.location.href = "FormRoommate.html";
              break;
              case "12":
                window.location.href = "FormRoommate.html";
              break;
          }
    
    }


