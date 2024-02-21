window.onload = function () {
    toggleMainSidebar()
};

function appendEventListener() {

    const listItems = document.querySelectorAll(".sidebar-list li");

    listItems.forEach((item) => {
        item.addEventListener("click", () => {
            let isActive = item.classList.contains("active");

            if (isActive) item.classList.remove("active");
            else item.classList.add("active");
        });
    });
}

const menuList = [
    {
        "name": "गृहपृष्ट",
        "icon": "food",
        "menu": [
            {
                "name": "ड्यासबोर्ड",
                "hasChildren": false,
                "children": null
            }
        ],
    },
   
];

// global variables
let locationPath = ''
async function fetchMenuInformation() {
    try {
        const response = await fetch('/Home/GetMenuInformation');
        if (response.ok) {
            const data = await response.json();
            menuList.length = 0; 
            menuList.push(...data); // Add the new menu items to the menuList array
            let currentLocation = ''
                locationPath = window.location.pathname
            menuList.forEach(resMenu => {
                resMenu.menu.forEach(subMenu => {
                    if (subMenu.hasChildren) {
                        if (subMenu.children.find(childrenMenu => childrenMenu.url == locationPath)){
                            currentLocation = resMenu
                        }
                    } else if (subMenu.url == locationPath){
                        currentLocation = resMenu
                    }
                })
                 
            })

            changeSubmenu(currentLocation.name) ;
            createMainSidebarMenu();
        } else {
            console.error('Failed to fetch menu information:', response.statusText);
        }
    } catch (error) {
        console.error('Error fetching menu information:', error);
    }
}

fetchMenuInformation().then(() => {
    createMainSidebarMenu();
});



var count =0
function changeSubmenu(menuName) {
    count++
    const currentMenu = menuList.find(res => res.name === menuName)
    console.log(currentMenu, count)
    toggleMainSidebar()
    if (currentMenu) {
        const secondarySidebar = document.getElementById('secondary-sidebar');
        secondarySidebar.innerHTML = ''
        const currentMenuDom = createMenuHTML(currentMenu)
        secondarySidebar.innerHTML = currentMenuDom
        if (count != 1 && screen.width <= 768) {
            secondarySidebar.style.display = "block"
        } 
        appendEventListener();
    }

}
function createMainSidebarMenu() {
    const primarySidebar = document.getElementById('p_sidebar');
    primarySidebar.innerHTML = ''
    let html = ` <div class="menu-list sidebar-menu" onclick="toggleMainSidebar()">
    <img class="sidebar-icon image-white" src="/images/menu-asset/icons/menu.png">
    <span class="sidebar-menu-name p-header">ई- हाजिरी</span>
  </div>`
    menuList.forEach(item => {
        html += `<div class="menu-list sidebar-menu" onClick="changeSubmenu('${item.name}')">
      <img class="sidebar-icon" src="/images/menu-asset/icons/${item.icon}.png">
      <span class="sidebar-menu-name">${item.name}</span></div>`
    })
    primarySidebar.innerHTML = html
}
function closeSecondarySidebar() {
    const secondarySidebar = document.getElementById('secondary-sidebar');
    secondarySidebar.style.display = "none"
}
function createMenuHTML(menuItems) {
    let html = `
      <div class="sidebar-list">
      <div class="active-menu-title">
          ${menuItems.name}
          <span class='close-btn-second' onclick="closeSecondarySidebar()">x</span>
      </div>
      <ul class="sidebar-list">`;
    menuItems.menu.forEach(item => {
        let active = ''

        if (item.hasChildren) {

            item.children.forEach(resChildren => {
                if (resChildren.url == locationPath) {
                    active = 'active'
                }
            })
            html += `<li class="dropdown ${active}">`;

            html += `<div class="title p-sub-title"><div class="child-title"><span class="name">${item.name}</span></div>`;
            html += `<img src="/images/menu-asset/icons/down-arrow.png" class="icon-down-arrow"></div>  <div class="submenu">`;
            item.children.forEach(children => {
                html += `<a href="${children.url}" class="sidebar-link ${children.url == locationPath ? 'active' : ''}">${children.name}</a>`
            })
            html += '</div>';
        } else {
            html += `<li class="dropdown ${item.url == locationPath ? 'active' : ''}">`;
            html += `<a class="title link" href="${item.url}"><span class="name">${item.name}</span></a>`;
        }
        html += '</li>';
    });
    html += '</ul> </div>';
    return html;
}

function toggleMainSidebar() {
    const mainSidebar = document.getElementById('p_sidebar');
    mainSidebar.classList.toggle('sidebar-active');
}



function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
}

// Close the dropdown if the user clicks outside of it
window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
        var dropdowns = document.getElementsByClassName("dropdown-content");
        var i;
        for (i = 0; i < dropdowns.length; i++) {
            var openDropdown = dropdowns[i];
            if (openDropdown.classList.contains('show')) {
                openDropdown.classList.remove('show');
            }
        }
    }
}
