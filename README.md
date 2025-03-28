<h2><b>Veterinary Clinic App</b></h2>

<p>This project is an ASP.NET MVC application designed to manage and review client details, specifically focusing on pet vaccination records. It utilizes a SQL Server database to store and retrieve information efficiently.</p>

<p>The primary purpose of this application is to streamline the process of managing client and pet data, ensuring that all relevant information is easily accessible and up-to-date. This helps in providing better care and service to pets by keeping accurate vaccination records and client details.</p>

<h2><b>Overview</b></h2>

<p>The first page users encounter when visiting the Veterinary Clinic App is the introduction page. This page is designed to provide an overview of the application's purpose and features to non-logged-in users. It serves as a welcome point, offering a brief introduction to the app's functionality and benefits.</p>

![Image](https://github.com/user-attachments/assets/ead13e15-a73d-440b-a041-05cf26c8e53b)

<h2><b>User's View</b></h2>

<p>When logged into the system, users accessing the Owners tab can view a comprehensive list of all active clients in the system. For each client, they have the option to click the Details button, which opens a dedicated page displaying detailed information about the client, including their personal profile and associated pets.</p>

![Image](https://github.com/user-attachments/assets/69643f15-adad-45a3-b943-d41aca851180)

![Image](https://github.com/user-attachments/assets/39dea090-fc67-442d-87fa-124c58e7e3ac)

<p>Users can view all vaccines and the pets that received them by clicking the Details button.</p>

![Image](https://github.com/user-attachments/assets/414e82f4-967e-4c51-8469-1cd784463e66)

![Image](https://github.com/user-attachments/assets/b1a0e967-92ad-44a7-8f3a-3753dd110c9f)

<p>Users can view all pets in the system, with each pet displaying a Details button that reveals additional information, including the pet’s owner and a list of vaccinations the pet has received.</p>

![Image](https://github.com/user-attachments/assets/f56469ac-b655-4cd7-868a-6a4a8f4f7c43)

![Image](https://github.com/user-attachments/assets/91bfd94f-3d39-4919-bbe8-8d53fdce72d3)\

<h2><b>Admin's View</b></h2>

<p>When an admin logs into the system, they are transferred to the Owners tab, which serves as a central hub for managing client information. </p> 

<p>This tab displays a table with essential details about each client, including their Name, Surname, and Age. Admins have the option to edit client information as needed, delete clients from the system if required, and view detailed profiles for each client, allowing for efficient management of client data and a deeper understanding of their interactions with the clinic.</p>

![Image](https://github.com/user-attachments/assets/00f6641e-cf05-4e5b-904d-ee3564c8e907)

<p>As mentioned, administrators have the capability to edit client information. This feature allows them to update details such as a client's Name, Surname, and Age as needed, ensuring that the data remains accurate and up-to-date.</p>

![Image](https://github.com/user-attachments/assets/4d5825b3-4078-4f98-8a7b-db16085c82ad)

<p>The administrator has the ability to delete clients from the system. When the delete button is clicked, a confirmation window appears, prompting the administrator to confirm the deletion of the specific client. This ensures that accidental deletions are prevented and allows the administrator to review the action before proceeding.</p>

![Image](https://github.com/user-attachments/assets/1ef6ecb7-75fa-426c-9101-1fb1dc60ede0)

<p>By clicking the Details button, the administrator can view comprehensive information about the client, including their personal details as well as information about their pets. This allows the administrator to manage client records efficiently and access all relevant data in one place.</p>

![Image](https://github.com/user-attachments/assets/f6686eb5-e3a1-4552-9900-e44b00189a01)

<p>In the Vaccines tab, administrators can view all vaccines stored in the system. Each vaccine entry includes three key actions: an Edit button to modify the vaccine’s name, a Delete button to remove a specific vaccine, and a Details button. Clicking Details reveals a list of all pets that have received the selected vaccine, enabling administrators to track vaccination history at the individual pet level. This streamlined interface ensures efficient management of vaccine records and their association with pets.</p>

![Image](https://github.com/user-attachments/assets/92610178-5716-4fa6-b6be-effb39636f8f)

![Image](https://github.com/user-attachments/assets/66711e1e-589d-40c8-b080-1363447e1c41)

![Image](https://github.com/user-attachments/assets/545c5548-0cbf-4345-bb73-45926be975a9)

![Image](https://github.com/user-attachments/assets/14cefb69-8acd-4aa2-9126-d5314815a3e3)

<p>The Pets tab provides a centralized overview of all pets registered in the system, displaying essential details for each animal. Users can view key information such as the pet’s Name, Age, and Owner</p>

![Image](https://github.com/user-attachments/assets/8e96f70f-077b-46d2-b749-f4ac2e798cc4)

![Image](https://github.com/user-attachments/assets/fe66b051-61b1-4be4-b623-07b10d14b459)

<p>As an administrator, you have the ability to edit key pet details including name, age, and owner information. Additionally, you can add vaccines to a pet’s record, with the system automatically filtering to show only vaccines the pet has not yet received.</p>

![Image](https://github.com/user-attachments/assets/a21309f3-0a9d-45fc-bcd5-623c55996a76)

<p>Administrators have the ability to delete pets through a confirmation process. When the delete button is clicked for a specific pet, a new window appears displaying all relevant information about the pet, including a final Delete button.</p>

![Image](https://github.com/user-attachments/assets/21d5339b-8d93-4972-89c6-e2fb585debfe)

<p>Administrators can access detailed pet information through a dedicated interface. When clicking the "Details" option, a new window opens to display comprehensive pet data, including list of administered vaccines.</p>

![Image](https://github.com/user-attachments/assets/f8061ce6-6e27-4a11-b142-e18ccd018cb9)

<p>Administrators possess the capability to create new client profiles, requiring mandatory entry of valid:</p>
<ul>
  <li>Name.</li>
  <li>Surname.</li>
  <li>Age.</li>
</ul>

![Image](https://github.com/user-attachments/assets/15056241-a055-4099-a3d4-096a8e0be107)

<p>Administrators can create new vaccines by entering a unique name for the vaccine.</p>

![Image](https://github.com/user-attachments/assets/c93a6436-33c8-45e5-b459-220711b7bc36)

<p>Administrators can create new pets by providing the following details:</p>
<ul>
  <li>Name.</li>
  <li>Age in range between 1 and 50.</li>
  <li>Choose the pet’s owner from a dropdown list of existing owners in the system.</li>
  <li>Select applicable vaccines from a dropdown list</li>
</ul>

![Image](https://github.com/user-attachments/assets/86328aea-60ab-4b43-8d18-45f747d5cae1)
