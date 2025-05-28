showBlog();
showBlogDetails();

function showBlog() {
    const url1 = "../json/Blog.json";
fetch(url1)
  .then((response) => response.json()) // parse the JSON from the server
  .then(function(posts) {
        var html = posts.map(function(post) {
            return `<div class="col-lg-4 col-md-6 wow fadeInUp" data-wow-delay="0.1s">
            <div class="room-item shadow rounded overflow-hidden">
                <div class="position-relative zoom">
                    <img class="img-fluid" src="${post.image_thumb}" alt="">
                </div>
                <div class="p-4 mt-2">
                    <div class="d-flex justify-content-between mb-3">
                        <h5 class="Tiêu đề bài đăng">${post.title}</h5>                                    
                    </div>
                    <div class="Thông tin demo">
                        <i class='fas fa-clock' style='font-size:px;color:rgb(75, 81, 137)'></i>

                                           <small class="Lượt xem">${post.day}</small>
                        &ensp;&ensp;&ensp;
                        <i class="fa fa-eye" style='font-size:px;color:rgb(72, 120, 133)'></i>     
                        <small class="Lượt xem">${post.view}</small>
                        &ensp;&ensp;&ensp;
                        <small class="Lượt xem">12&ensp;</small>    
                        <i class="fa fa-heart" style="font-size:px;color:red"></i>

                    </div>
                   
                </br>
                    <p class="Thông tin">${post.review}</p>
                    <div class="Xem chi tiết"><a class="Xem chi tiết" onclick="Details(${post.id})">Xem chi tiết</a></div>
                </div>
            </div>
        </div>`;
  })
                 var htmls = html.join('');    
                 document.getElementById("blog").innerHTML = htmls;
})
}

function showBlogDetails() {
    const url3 = "../json/Blog.json";
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
    var info = detailpost.map(function(post) {
       
        return ` <div class="Thông tin demo">
        <i class='fas fa-clock' style='font-size:px;color:rgb(75, 81, 137)'></i>        
        <small class="Lượt xem">${post.day}</small>
        &ensp;&ensp;&ensp;
        <i class="fa fa-eye" style='font-size:px;color:rgb(72, 120, 133)'></i>     
        <small class="Lượt xem">${post.view}</small>
        &ensp;&ensp;&ensp;          
    </div>
    </br>
    </br>
    <h1 align="center" >${post.title}</h1>
    </br>
    </br>                      
    <p class="p-1" style="text-align:justify">${post.p1[0]}</p>                      
    </br>
    <p class="p-1" style="text-align:justify">${post.p1[1]}</p>                                                                                                                                                                                                       
    </br>
    <center><img src="${post.image_1}" width="100%"></center> 
    </br>    
        <i><center><p style="font-size:18px">${post.image_title}</p></center></i>
    </br>
        <p class="p-1" style="text-align:justify">${post.p2[0]}</p>
    </br>
    <p class="p-1" style="text-align:justify">${post.p2[1]}</p>
    </br>
        <p class="p-1" style="text-align:justify">${post.p2[2]}</p>
        <p class="p-1" style="text-align:justify">${post.p2[3]}</p>

        <i><p style="text-align:right;font-size:18px" >Được tổng hợp bởi Đội ngũ Moro</p></i>  </div>` ; 
    })
    
    document.getElementById("BlogDetail").innerHTML = info;

   
  })
}
function Details(id) {
    localStorage.setItem("ID_POST", id);
    window.location.href = "Blog_Detail.html";
    }

