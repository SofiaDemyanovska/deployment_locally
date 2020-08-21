pipeline {
    agent {
      node {
        label 'slave1'
      }
    }

    stages{
                stage('Build backend') {
                    steps {
                       sh '''
      		        cd backend-dubbing-project
			dotnet publish -c Release -o out
			mkdir Web/out/AudioFiles             		
			'''
		   }
		}
		stage('Migration') {
 		    steps {
			sh '''
			cd backend-dubbing-project/Web/
           		dotnet ef migrations add Migration${BUILD_NUMBER}
              		dotnet ef database update
			cp dubbing.db out/
			'''
		   }
                }
                stage('Build frontend') {
                    steps {
			sh '''
                	cd front-dubbing-project
                	npm i
			npm run build
                        '''
                    }
                }


	stage('Run Tests') {
            parallel {
                stage('Administration Test') {
                    steps {
                        sh "dotnet test backend-dubbing-project/Administration/UnitTests/UnitTests.csproj" 
                    }
                }
                stage('Streaming Test') {
                    steps {
                        sh 'dotnet test backend-dubbing-project/Streaming/UnitTests/UnitTests.csproj'
                     }
                }
            }
        }



        stage('Run dubbing progect') {
            parallel {
                stage('Run backend') {
                    steps {
                        sh '''
			cd backend-dubbing-project/Web/out/
			nohup dotnet Web.dll --urls http://10.26.1.88:5000 > nohup.out 2> nohup.err < /dev/null &
			'''
                    }
                }
                stage('Run frontend') {
                    steps {
                        sh '''
			cp -r front-dubbing-project/build/* /usr/share/nginx/html/
			'''
                     }
                }
            }
        }

    }

     post {
       always {
          cleanWs()
       }
     }
}
