namespace GXPEngine.Core
{
    public struct Vector2
    {
        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            return obj is Vector2 other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (x.GetHashCode() * 397) ^ y.GetHashCode();
            }
        }

        public float x;
        public float y;

        //CONSTRUCTOR	
        /// <summary>
        /// Makes a vector based on a given x and y value or an existing vector
        /// </summary>
        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2(Vector2 vector)
        {
            x = vector.x;
            y = vector.y;
        }

        //STRING	
        public override string ToString()
        {
            return "[Vector2 " + x + ", " + y + "]";
        }
        
        //+
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x + right.x, left.y + right.y);
        }
        
        //-
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.x - right.x, left.y - right.y);
        }
        
        public static bool operator !=(Vector2 left, Vector2 right)
        {
            if (left.x != right.x || left.y != right.y)
            {
                return true;
            }
            else return false;
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            if (left.x == right.x && left.y == right.y)
            {
                return true;
            }

            return false;

        }

        //ADDITION
        /// <summary>
        ///     Adds a vector onto this one, can be used with an x and y or another vector
        /// </summary>
        public void Add(float plusX, float plusY)
        {
            this.x += plusX;
            this.y += plusY;
        }

        public void Add(Vector2 vector)
        {
            Add(vector.x, vector.y);
        }

        //MULTIPLICATION	
        /// <summary>
        ///     Multiplies the x and y value of the vector by a given scalar
        /// </summary>
        public void Multiply(float m)
        {
            x *= m;
            y *= m;
        }

        public static Vector2 Multiply(Vector2 v, float m)
        {
            return new Vector2(v.x * m, v.y * m);
        }

        //SET
        /// <summary>
        ///     Sets the vector to a given x and y value
        /// </summary>
        public void Set(float newX, float newY)
        {
            this.x = newX;
            this.y = newY;
        }

        //DIVIDATION	

        /// <summary>
        ///     Divides the x and y value of the vector by a given scalar
        /// </summary>
        public void Divide(float d)
        {
            if (d != 0)
            {
                x /= d;
                y /= d;
            }
        }

        /// <summary>
        ///     Divides the x and y value of the vector by a scalar and returns a new vector
        /// </summary>
        public static Vector2 Divide(Vector2 v, float d)
        {
            if (d != 0)
                return new Vector2(v.x / d, v.y / d);
            return v;
        }

        //MAGNITUDE
        /// <summary>
        ///     Returns the magnitude of the vector,
        ///     the magnitude is a scalar that indicates the length of the vector
        /// </summary>
        public float Magnitude()
        {
            var a = Mathf.Square(x);
            var b = Mathf.Square(y);
            var c = a + b;

            return Mathf.Sqrt(c);
        }

        /// <summary>
        ///     Set the magnitude of a vector and change it accordingly
        /// </summary>
        public void SetMagnitude(float m)
        {
            Normalize();
            Multiply(m);
        }

        //NORMAL_VECTOR	
        /// <summary>
        ///     Returns the normal vector of this vector
        /// </summary>
        public Vector2 Normal()
        {
            return new Vector2(y, -x);
        }

        //NORMALIZE	
        /// <summary>
        ///     Normalizes the vector to a length of 1
        /// </summary>
        public void Normalize()
        {
            var m = Magnitude();
            if (m != 0) Divide(m);
            //else Console.WriteLine("Magnitude = 0, can't be normalized");
        }

        /// <summary>
        ///     Normalizes the vector to a length of 1 and returns it as a new vector
        /// </summary>
        public static Vector2 Normalize(Vector2 v)
        {
            var m = v.Magnitude();
            if (m != 0) return Divide(v, m);
            //Console.WriteLine("Magnitude = 0, can't be normalized"); 
            return v;
        }

        //ARRAY	
        /// <summary>
        ///     Returns the contents of the vector in an array
        /// </summary>
        public float[] Array()
        {
            float[] array = {x, y};
            return array;
        }
    }
}