using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gradeprogram.Models;

namespace gradeprogram.Service
{
    public class ORCScomparer: IEqualityComparer<ORCS_FileUploadData>
    {
        public bool Equals(ORCS_FileUploadData x, ORCS_FileUploadData y)
        {

            //Check whether the compared objects reference the same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Check whether any of the compared objects is null.
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Check whether the products' properties are equal.
            return x.cExerciseCondID == y.cExerciseCondID && x.cExerciseID == y.cExerciseID;
        }
    
        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for these objects.

        public int GetHashCode(ORCS_FileUploadData ORCSobj)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(ORCSobj, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashORCSobjExerCondid = ORCSobj.cExerciseCondID == null ? 0 : ORCSobj.cExerciseCondID.GetHashCode();

            //Get hash code for the Code field.
            int hashORCSobjExerid = ORCSobj.cExerciseID.GetHashCode();

            //Calculate the hash code for the product.
            return hashORCSobjExerCondid ^ hashORCSobjExerid;
        }

    }
}