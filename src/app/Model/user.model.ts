export class User {
    id: number;
    name: string;
    email: string;
    password: string;
    roleId: number;
    deptId: number;
    loginCount: number;
    createdDate: Date;
    passwordHash: string;
    passwordSalt: string;
    department?: { deptName: string }; // Optional, for department name
  
    constructor(
      id: number = 0,
      name: string = '',
      email: string = '',
      password: string = '',
      roleId: number = 0,
      deptId: number = 0,
      loginCount: number = 0,
      createdDate: Date = new Date(),
      passwordHash: string = '',
      passwordSalt: string = '',
      department: { deptName: string } = { deptName: '' }
    ) {
      this.id = id;
      this.name = name;
      this.email = email;
      this.password = password;
      this.roleId = roleId;
      this.deptId = deptId;
      this.loginCount = loginCount;
      this.createdDate = createdDate;
      this.passwordHash = passwordHash;
      this.passwordSalt = passwordSalt;
      this.department = department;
    }
  }
  