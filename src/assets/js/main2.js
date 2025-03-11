function toggleForms() {
  var selectedOption = document.getElementById("optionSelect").value;

  document.getElementById("editCourseForm").style.display = "none";
  document.getElementById("moduleActionForm").style.display = "none";
  document.getElementById("addModuleForm").style.display = "none";
  document.getElementById("editModuleForm").style.display = "none";
  document.getElementById("topicActionForm").style.display = "none";
  document.getElementById("addTopicForm").style.display = "none";
  document.getElementById("editTopicForm").style.display = "none";

  // Show the selected form
  if (selectedOption === "editCourse") {
      document.getElementById("editCourseForm").style.display = "block";
  } else if (selectedOption === "editModule") {
      document.getElementById("moduleActionForm").style.display = "block";
  } else if (selectedOption === "editTopic") {
      document.getElementById("topicActionForm").style.display = "block";
  }
}

function showModuleForm(action) {
document.getElementById("addModuleForm").style.display = action === "add" ? "block" : "none";
document.getElementById("editModuleForm").style.display = action === "edit" ? "block" : "none";
}

function showTopicForm(action) {
document.getElementById("addTopicForm").style.display = action === "add" ? "block" : "none";
document.getElementById("editTopicForm").style.display = action === "edit" ? "block" : "none";
}
