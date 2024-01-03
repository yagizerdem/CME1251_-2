var express = require('express')
var cors = require('cors')
var http = require('http')
var bodyParser = require('body-parser')
var app = express() // the main app
http.createServer(app).listen(8000)
var randomstring = require("randomstring");
var fs = require('fs');

app.use(express.static('./public'));
app.use(cors())

const ADMINUSERNAME = "admin"
const ADMINPASSWORD = "1234"

app.use(express.json());
  

app.get('/', function (req, res) {
    res.send("test data")
})

app.get('/download' , function(req ,res){
    const file = `${__dirname}/public/Dll/gamedllfiles.rar`;
    res.setHeader('Content-type','application/zip');
    res.sendFile(file);
})
app.post("/login" , (req ,res) =>{
    const username = req.body.username
    const password = req.body.password
    
    console.log(req)

    if(username == ADMINUSERNAME && password == ADMINPASSWORD){
        const cookie = randomstring.generate();
        console.log(__dirname)
        res.json({success:true , cookie: cookie })
        async function appendFile(data){
            var result = await fs.appendFile(`${__dirname}/Context/cookies.txt` , data + "\n" , (err)=>{
                console.log(err)
            })
        }
        appendFile(cookie)
        return
    }
    res.json({success:false})
})

app.get('/api/playerscores' , (req ,res)=>{
     // returning socres 
     const path = `${__dirname}/Context/gameScores.json`
     fs.readFile(path , 'utf-8',(err,data)=>{
        if(err){
            console.log(err)
            res.json({success:false})
        }
        else{
            if(data == null || data == undefined || data.trim() == "" ){
                data = "{}"
            }
            data = JSON.parse(data)
            res.json({success:true , data:data})
        }
     })
})
app.post('/api/logplayerscores', (req, res) => {
    // storing player socres
    const path = `${__dirname}/Context/gameScores.json`
    const username = req.body.username
    const score = req.body.score
    fs.readFile(path , 'utf-8',(err,data)=>{
        if(err){
            console.log(err)
            res.json({success:false})
        }
        else{
            if(data == null || data == undefined || data.trim() == "" ){
                data = "{}"
            }
            data = JSON.parse(data)
            data[username] = score
            data = JSON.stringify(data)
            fs.writeFile(path , data, (err)=>{
                if(err){
                    console.log(err)
                    res.json({success:false})
                }
                res.json({success:true})
            })

        }
     })


    
  })

app.post('/api/delterecord',(req ,res) =>{
    var cookie = req.body.cookie
    var newrecord = req.body.newrecord

    fs.readFile(`${__dirname}/Context/cookies.txt` ,"utf-8" ,(err ,data)=>{
        const allCookiesfromdb = data.split("\n")
        var flag = false
        for(var item of allCookiesfromdb){
            if(item.trim() == "") continue
            if(item == cookie){
                flag = true
                break
            }
        }
        if(flag == false){
            res.send({success:false , message:"you are not logged in"})
            return
        }
        newrecord = JSON.stringify(newrecord)
        fs.writeFile(`${__dirname}/Context/gameScores.json` , newrecord, (err)=>{
            if(err){
                console.log(err)
                res.json({success:false})
            }
            res.json({success:true})
        })
    })

})
