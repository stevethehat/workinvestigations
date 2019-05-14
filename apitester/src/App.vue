<template>
    <div id="app">
        <div>
            <div class="fixed-top">
                 
                <!--
                <top-nav-bar></top-nav-bar>
                <breadcrumb-bar></breadcrumb-bar>
                -->
                <h1 id="pageTitle">Login</h1>
                <!--
                <settings-panel></settings-panel>
                -->
                <a id="home" href="/">
                    <img class="logo" src="@/assets/gold_ring.png">
                </a>
            </div>

            <div class="content-container container-fluid">
                <div class="row">
                    <!--<side-nav-bar></side-nav-bar>-->
                    <main role="main" id="main" class="col-md-10 ml-sm-auto col-lg-10 py-3 px-4 main" >
                        <router-view></router-view>
                    </main>
                </div>
            </div>
        </div>
    </div>      
</template>

<style lang="scss">
#app {
    font-family: 'Ubuntu', 'Avenir', Helvetica, Arial, sans-serif;
    font-size: 15px;
    -webkit-font-smoothing: antialiased;
    -moz-osx-font-smoothing: grayscale;
    color: #2c3e50;
    padding: 0;
    margin: 0;
    border: none;
}

body {
  font-size: .875rem;
  overflow: hidden;
}

.logo {
    position: absolute;
    top: 20px;
    left: 45px;
}



.content-container {
    padding-top: 110px;
    padding-bottom: 70px;
}

.main {
    overflow-y: scroll;

    /*Total viewport height minus the height of the bottom bar
    and the fixed top bar.*/
    height: calc(100vh - 140px);
}

/*
 * Utilities
 */

.border-top { border-top: 1px solid #e5e5e5; }
.border-bottom { border-bottom: 1px solid #e5e5e5; }

.mainDiv{
    height:calc(100vh - 220px);
}

.fixed-top{
    background-color: black;
    height: 100px;
}
</style>
