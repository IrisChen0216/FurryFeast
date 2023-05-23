new Vue({
    el: '#app',
    data: {
        recipes: []
    },
    mounted: function() {
        var self = this;
        axios.post('GetAllRecipes').then(res => { console.log(res.data); });

    },
    methods: {
        
    }
});