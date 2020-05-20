import Vue from 'vue';
import { TinymceVue } from '../src/';

Vue.component('tinymce-vue', TinymceVue);

var vm = new Vue({
    el: '#app',
    data: function(){
        return {
            title: 'VueTinymce',
            data: 'sss',
            id: 'fuck'
        }
    }
})