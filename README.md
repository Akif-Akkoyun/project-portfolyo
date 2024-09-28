# project-portfolyo

Technologies: .NET 8, EF Core, Onion Architecture, FluentValidation, HttpClient, JWT Authentication/Authorization

I designed this project, which I developed using .NET 8 and Entity Framework Core, as my personal portfolio site. I chose Onion Architecture in the project and moved the data in the entities with DTOs. I performed data validation with FluentValidation.

I created services to manage requests to APIs, and these services can process and route requests coming via HttpClient. Additionally, I successfully integrated Authentication and Authorization processes with JWT in the project. Users can perform operations such as login, register and forgot password. For the password reset process, a link is sent to the user from the e-mail address created by the project, and the password reset can be done via this link.

The project has an Admin panel. Here I can do basic CRUD operations and have the authority to change user roles. I also developed a blog sharing system. Only logged in users can access blog posts, and users without admin privileges cannot log in to the admin panel.

With this project, I aimed to create a strong structure by applying the most up-to-date technologies used in modern web applications.
